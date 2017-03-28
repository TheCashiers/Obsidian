import create from "react-test-renderer";
import React from 'react';
import { UserCreationContainer } from "../containers/UserCreationContainer";

test("throw exceptions when token is undefined", () => {
    expect(new UserCreationContainer({ token: undefined })).toThrowError();
})