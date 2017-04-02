/// <reference path="../../node_modules/axios/axios.d.ts" />
/// <reference path="../../node_modules/@types/jest/index.d.ts"/>
//add declarations for snackbarjs and material.js
import { ComponentClass } from 'react';
interface JQueryStatic{
    material:MaterialJS;
}

interface MaterialJS{
    init():Function;
}