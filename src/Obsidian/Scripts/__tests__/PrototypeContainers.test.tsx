import create from "react-test-renderer";
import React from 'react';
import { FormContainer } from "../containers/FormContainer";

class myContainer extends FormContainer{
}

test("throw exceptions when token is undefined", () => {
    expect(new myContainer({ token: undefined })).toThrowError();
})

