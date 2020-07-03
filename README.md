

# Proyecto GoGoSushi

El proyecto se compone de dos partes, una es la capa de servicios que fue desarrollada en C# usando la Arquitecura SOA y utilizando los metodos de WebApi para crear Servicios Rest Full y la otra es la CapaUI usando ionic 5, platoforma Cross-Platform para el desarrollo de apps moviles, este  se compone principalmente de Angular y un SDK nativo que es Cordova.

## WebService

Para Correr el proyecto se debe ejecutar primeramente el script sql adjuntado en este proyecto, luego se debe cambiar la cadena de conexion ubicado en **CapaServicio** / **Conexion.cs**.

```csharp
    private readonly String cadenaConexion = "<TuCadenaDeConexion>";
```

Seleccionar como proyecto de inicio el proyecto "WebServices" y iniciar el ISS Express.

## CapaUI
Para la capa de UI se necesita prepara el entorno de desarrollo para nodeJS, visitar https://nodejs.org/es/
Para verificar que node este bien instalado, abrir una terminal y ejecutar ```npm --version``` esto debiera devolver la version de node instalada.

### Preparando entorno de IONIC

Instalar ionic de forma global 
```
npm install -g ionic
```

Instalar cordova de forma global
```
npm install -g cordova
```

Para instalar las dependencias del proyecto debe ingresar por la terminal hasta la raiz de este, una vez ahi ejecutar 
```
npm install
```
Lo que procedera a crear una carpeta llamada node_modules donde se guardaran todas las dependencias especificadas en el package.json del proyecto.

## Correr en el Browser
Todo instalado ya se puede correr el proyecto de forma local para esto se debe ejecutar
```
ionic serve
```
Esto levantara el proyecto en un server de node el cual podra ser visto en http://localhost:8100/, si requiere especificar el puerto donde desea correr el proyecto ejecutar 
```
ionic serve --port <PuertoAUtilizar>
```

Se recomienda abrir el inspector de elementos y presionar la combinacion de teclas Ctrl + Shift + M, para visualizar en vista movil

--------------------

## Correr en el dispositivo
Si desea correr el proyecto en el dispositivo, se debe preparar el entorno para la plataforma que desea ejecutar (Android y/o iOS).
Se recomiendan los siguientes post para realizar estos pasos

- Android
https://medium.com/@thianlopezz/c%C3%B3mo-instalar-android-sdk-y-configurarlo-para-poder-probar-mis-aplicaciones-de-ionic-framework-49bac6e4323b

- iOS
https://code.tutsplus.com/es/tutorials/ionic-from-scratch-getting-started-with-ionic--cms-29862

NOTA: Para iOS se necesita de un MAC y tener instalada la ultima version de Xcode para poder compilar aplicaciones para iPhone. 
Para android es necesario tener instalado Android studio o instalar solo los Gradle para compilar el proyecto desde la terminal de ionic.


## Autores ✒️

- Daniel Marquez. 
- Eleodoro Pareja. 