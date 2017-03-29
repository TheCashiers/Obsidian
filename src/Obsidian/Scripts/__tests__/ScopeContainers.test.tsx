import create from "react-test-renderer";
import * as React from 'react';
import { shallow, mount } from 'enzyme';
import { ScopeCreationContainer } from "../containers/ScopeCreationContainer";
import { ScopeForm } from "../components/Form";

test("throw exceptions when token is undefined", () => {
    expect(() => { new ScopeCreationContainer({}) }).toThrowError();
});


test("handle submit function get called", () => { 
    let mockSubmit = jest.fn();
    let scopeForm = mount(<ScopeForm onSubmit={mockSubmit} />);
    expect(scopeForm.props().onSubmit).toBeDefined();
    const form = scopeForm.find('form').first();
    const input = scopeForm.find('input').first();
    form.simulate('submit');
    expect(mockSubmit).toBeCalled();
});

test("forms get right input from users", () => {
    let mockChange = jest.fn();
    let scopeForm = mount(<ScopeForm onInputChange={mockChange}/>);
    const input = scopeForm.find('[name="scopeName"]').first();
    input.simulate('change', { target: { value: 'Scope #1' } });
    expect(mockChange.mock.calls[0][0]["target"]).toEqual({ value: 'Scope #1'  });
})

test("containers received new state", () => {
    let container = mount(<ScopeCreationContainer token="MockedToken" />);
    const input = container.find('[name="scopeName"]').first();
    input.simulate('change', { target: { name:"scopeName", value: 'Scope #1' } });
    expect(container.state("scopeName")).toBe("Scope #1");
})