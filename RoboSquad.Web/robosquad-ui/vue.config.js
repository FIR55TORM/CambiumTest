const { defineConfig } = require('@vue/cli-service');
const path = require("path");

module.exports = defineConfig({
  filenameHashing: false,
  outputDir: path.resolve(__dirname, "../wwwroot/"),
  transpileDependencies: true
})
