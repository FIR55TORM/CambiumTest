<template>
    <div>
        <label for="rangeX" class="form-label">Range For X coordinates: {{ gridSizeX }}</label>
        <input v-model="gridSizeX" type="range" class="form-range" min="5" max="10" id="rangeX">

        <label for="rangeY" class="form-label">Range For Y coordinates: {{ gridSizeY }}</label>
        <input v-model="gridSizeY" type="range" class="form-range" min="5" max="10" id="rangeY">

        <button class="btn btn-primary" @click="$emit('onGridSizeSelected', {x: gridSizeX, y: gridSizeY})">Next: Execute Commands</button>
    </div>
</template>

<script>
export default {
    name: "AdjustGridSize",
    data() {
        return {
            gridSizeX: "5",
            gridSizeY: "5",
        }
    },

}
</script>