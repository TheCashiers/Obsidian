/// <reference path="../../node_modules/axios/axios.d.ts" />
/// <reference path="../../node_modules/@types/jest/index.d.ts"/>
//add declarations for snackbarjs and material.js
interface JQueryStatic{
    material:MaterialJS;
    snackbar:Function;
}

interface MaterialJS{
    init():Function;
}