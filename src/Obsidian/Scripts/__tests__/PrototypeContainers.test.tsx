import React from "react";
import create from "react-test-renderer";
import { FormContainer } from "../containers/FormContainer";

class MyContainer extends FormContainer {}

test("throw exceptions when token is undefined", () => {
    expect(() => { new MyContainer({ props: undefined }); }).toThrowError();
});
