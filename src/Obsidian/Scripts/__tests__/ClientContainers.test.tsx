import create from "react-test-renderer";
import React from 'react';
import { ClientCreationContainer } from "../containers/ClientCreationContainer";

test("throw exceptions when token is undefined", () => {
    expect(new ClientCreationContainer({ token: undefined })).toThrowError();
})