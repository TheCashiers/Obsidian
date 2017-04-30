import { mount, shallow } from "enzyme";
import * as React from "react";
import create from "react-test-renderer";
import { ScopeForm } from "../components/Form";
import { ScopeCreationContainer } from "../containers/ScopeCreationContainer";

test("throw exceptions when token is undefined", () => {
    expect(() => { new ScopeCreationContainer({}); }).toThrowError();
});

test("handle submit function get called", () => {
    const mockSubmit = jest.fn();
    const scopeForm = mount(<ScopeForm onSubmit={mockSubmit} />);
    expect(scopeForm.props().onSubmit).toBeDefined();
    const form = scopeForm.find("form").first();
    const input = scopeForm.find("input").first();
    form.simulate("submit");
    expect(mockSubmit).toBeCalled();
});

test("forms get right input from users", () => {
    const mockChange = jest.fn();
    const scopeForm = mount(<ScopeForm onInputChange={mockChange}/>);
    const input = scopeForm.find('[name="scopeName"]').first();
    input.simulate("change", { target: { value: "Scope #1" } });
    expect(mockChange.mock.calls[0][0]["target"]).toEqual({ value: "Scope #1"  });
});

test("containers received new state", () => {
    const container = mount(<ScopeCreationContainer token="MockedToken" />);
    const input = container.find('[name="scopeName"]').first();
    input.simulate("change", { target: { name: "scopeName", value: "Scope #1" } });
    expect(container.state("scopeName")).toBe("Scope #1");
});
