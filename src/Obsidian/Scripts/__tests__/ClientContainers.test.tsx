import {mount, shallow} from "enzyme";
import * as React from "react";
import create from "react-test-renderer";
import {ClientForm} from "../components/Form";
import {ClientCreationContainer} from "../containers/ClientCreationContainer";
import { INamedEvent } from "../containers/FormContainer";

const mockChange = jest.fn<INamedEvent>();
const mockSubmit = jest.fn();

test("handle submit function get called", () => {
    const clientForm = mount(
        <ClientForm
            onSubmit={mockSubmit}
            displayName=""
            redirectUri=""
            action="mockAction"
        />);
    expect(clientForm.props().onSubmit).toBeDefined();
    const form = clientForm.find("form").first();
    const input = clientForm.find("input").first();
    form.simulate("submit");
    expect(mockSubmit).toBeCalled();
});

test("forms get right input from users", () => {
    const clientForm = mount(
        <ClientForm
            onSubmit={mockSubmit}
            displayName=""
            redirectUri=""
            action="mockAction"
            onInputChange={mockChange}
        />);
    const input = clientForm.find('[name="displayName"]').first();
    input.simulate("change", { target: { value: "Client #1" } });
    const targetStr = "target";
    expect(mockChange.mock.calls[0][0][targetStr]).toEqual({ value: "Client #1" });
});

test("containers received new state", () => {
    const container = mount(<ClientCreationContainer token="MockedToken" />);
    const input = container.find('[name="displayName"]').first();
    input.simulate("change", { target: { name: "displayName", value: "Client #1" } });
    expect(container.state("displayName")).toBe("Client #1");
})
;
