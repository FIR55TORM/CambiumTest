<template>
  <div>
    <h1>RoboSquad</h1>
    <h2>By your command...</h2>
    <br>

    <UploadFile v-show="currentStep === 1" @onFileSelected="onFileSelected" />
    <AdjustGridSize v-show="currentStep === 2" @onGridSizeSelected="onGridSizeSelected" />
    <PlateauRoverPositions v-show="currentStep === 3 && !hasError" :gridSize="payload.gridSize" :isLoading="isLoading"
      :gridData="gridData" />

    <div v-if="hasError">
      <p>An error occurred and could not run commands. Please make sure the grid size is big enough.</p>

      <a href=" " class="btn btn-primary" @click="window.location.href = '/'">Restart</a>
    </div>

  </div>
</template>

<script>
const axios = require('axios');

import UploadFile from "./components/UploadFile.vue";
import AdjustGridSize from "./components/AdjustGridSize.vue";
import PlateauRoverPositions from "./components/PlateauRoverPositions.vue";

export default {
  name: 'App',
  data() {
    return {
      currentStep: 1,
      payload: {},
      isLoading: true,
      hasError: false,
      gridData: {}
    }
  },
  methods: {
    onFileSelected(movementFile) {
      this.payload.movementFile = movementFile;
      this.currentStep = 2;
    },
    onGridSizeSelected(gridSize) {
      this.payload.gridSize = gridSize;
      this.currentStep = 3;
      this.executeCommands();
    },
    executeCommands() {
      var vm = this;
      const detailsDto = { X: vm.payload.gridSize.x, Y: vm.payload.gridSize.y };
      const detailsDtoJsonString = JSON.stringify(detailsDto);

      /*
        Left for my future reference, Because of time constraints, I had to send the grid data as a json string instead :( 
          Need to figure out why this wasn't deserializing at the other end...
        */
      /* const detailsDtoBlob = new Blob([detailsDto], {
         type: 'application/json'
       });

      vm.payload.movementFile.append("detailsDto", detailsDtoBlob); */

      vm.payload.movementFile.append("detailsDtoJson", detailsDtoJsonString);

      axios.post('/api/rover/ExecuteCommands/', vm.payload.movementFile,
        {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        }
      )
        .then(function (response) {
          // handle success
          vm.isLoading = true;
          vm.gridData = response.data
        })
        .catch(function (error) {
          // handle error
          console.log(error);
          vm.hasError = true;
        })
        .then(function () {
          // always executed
          vm.isLoading = false;
        });
    }
  },
  components: {
    UploadFile,
    AdjustGridSize,
    PlateauRoverPositions
  }
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
