import { mount, shallow } from "enzyme";
import * as React from "react";
import create from "react-test-renderer";
import { UserForm } from "../components/Form";
import { UserCreationContainer } from "../containers/UserCreationContainer";

const mockChange = jest.fn();
const mockSubmit = jest.fn();
const userForm = mount(<UserForm onSubmit={mockSubmit} onInputChange={mockChange} />);
const container = mount(<UserCreationContainer token="MockedToken" />);

test("throw exceptions when token is undefined", () => {
    expect(() => { new UserCreationContainer({}); }).toThrowError();
});

test("handle submit function get called", () => {
    expect(userForm.props().onSubmit).toBeDefined();
    const form = userForm.find("form").first();
    const input = userForm.find("input").first();
    form.simulate("submit");
    expect(mockSubmit).toBeCalled();
});

test("forms get right input from users", () => {
    const input = userForm.find('[name="username"]').first();
    input.simulate("change", { target: { value: "User #1" } });
    expect(mockChange.mock.calls[0][0]["target"]).toEqual({ value: "User #1" });
});

test("containers received new state", () => {
    const input = container.find('[name="username"]').first();
    input.simulate("change", { target: { name: "username", value: "User #1" } });
    expect(container.state("username")).toBe("User #1");
});
