<template>
    <div>
        <h3>Step - 1</h3>
        <p>Upload Movement Commands. </p>
        <div class="text-center">
            <div>
                <label for="formFileLg" class="form-label">Upload Movement Commands File</label>
                <input class="form-control form-control-lg" id="formFileLg" type="file" @change="onFileSelected">
            </div>

            <button class="btn btn-primary" :disabled="!fileSelected"
                @click="$emit('onFileSelected', movementsFile)">Next: Adjust Grid</button>

        </div>
    </div>
</template>

<script>
export default {
    name: "UploadFile",
    data() {
        return {
            movementsFile: null,
            fileSelected: false
        }
    },
    methods: {
        onFileSelected(event) {
            var files = event.target.files || event.dataTransfer.files;
            if (!files.length)
                return;

            this.fileSelected = true;

            this.movementsFile = new FormData();

            this.movementsFile.append("movementsFile", files[0], files[0].name);

            //this.movementsFile = files;
        }
    }

}
</script>