import create from "react-test-renderer";
import * as React from 'react';
import { shallow, mount } from 'enzyme';
import { ClientCreationContainer } from "../containers/ClientCreationContainer";
import { ClientForm } from "../components/Form";

test("throw exceptions when token is undefined", () => {
    expect(() => { new ClientCreationContainer({}) }).toThrowError();
});


test("handle submit function get called", () => {
    let mockSubmit = jest.fn();
    let clientForm = mount(<ClientForm onSubmit={mockSubmit} />);
    expect(clientForm.props().onSubmit).toBeDefined();
    const form = clientForm.find('form').first();
    const input = clientForm.find('input').first();
    form.simulate('submit');
    expect(mockSubmit).toBeCalled();
});

test("forms get right input from users", () => {
    let mockChange = jest.fn();
    let clientForm = mount(<ClientForm onInputChange={mockChange} />);
    const input = clientForm.find('[name="displayName"]').first();
    input.simulate('change', { target: { value: 'Client #1' } });
    expect(mockChange.mock.calls[0][0]["target"]).toEqual({ value: 'Client #1' });
})

test("containers received new state", () => {
    let container = mount(<ClientCreationContainer token="MockedToken" />);
    const input = container.find('[name="displayName"]').first();
    input.simulate('change', { target: { name: "displayName", value: 'Client #1' } });
    expect(container.state("displayName")).toBe("Client #1");
})