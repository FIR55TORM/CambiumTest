<p align="center">
  <a href="" rel="noopener">
 
</p>

<h1 align="center">RoboSquad</h1>

<div align="center">

[![Status](https://img.shields.io/badge/status-active-success.svg)]()
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/kylelobo/The-Documentation-Compendium.svg)](https://github.com/kylelobo/The-Documentation-Compendium/pulls)

</div>

---

<p align="center"> Upload a commands csv file and move your rovers on a grid
    <br> 
</p>

## 📝 Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [Usage](#usage)
- [Built Using](#built_using)
- [Authors](#authors)
- [Acknowledgments](#acknowledgement)

## 🧐 About <a name = "about"></a>

Cambium Technical Test

## 🏁 Getting Started <a name = "getting_started"></a>

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Installed copies of Visual Studio Code or Visual Studio 2022

Node.js LTS

### Installing

Builds frontend assets by navigating to "RoboSquad.Web > robosquad-ui" using your favorite terminal app (I like powershell), then run:

```
$ npm install
```

then

```
$ npm run build
```

Please make sure to run the solution in VS or VS code, making sure the start up project is Robosquad.Web.

you can either run it from Visual Studio 2022 via IIS express (you may have to add a self signed ssl certificate) or from the dotnet cli as below:

Navigate to

```
cd\<your-repo-dir>\RoboSquad\RoboSquad.Web\
```

then run

```
dotnet run
```

then navigate to the shown url.

## 🔧 Running the tests <a name = "tests"></a>

You can run xunit tests within Visual Studio. Unfortunately there are now frontend unit tests however Jest was installed with the intention of producing some frontend unit tests.

## ⛏️ Built Using <a name = "built_using"></a>

- [VueJs](https://vuejs.org/) - Web Framework
- Vite, Webpack and Babel (via vue-cli)
- [NodeJs](https://nodejs.org/en/) - Server Environment
- Asp.Net Core using .Net 6 - Backend

## ✍️ Authors <a name = "authors"></a>

- Ediz Aziz

## 🎉 Acknowledgements <a name = "acknowledgement"></a>

- Thanks for the inspiration, Have I had a little more time (and spent less time on other areas) I would of liked to do more like:

  - Frontend Unit testing
  - Integration Tests via xUnit
  - Upload additional commands
  - Additional UI elements like tooltips, graphics etc.

