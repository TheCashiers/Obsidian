import * as React from "react";
import { ClientList } from "../components/ClientManagement"
import { MaterialForm, MaterialInput } from "../components/Form"
import { create } from 'react-test-renderer';
import { PortalHeader, UserInfo } from "../components/PortalElements";


it('ClientList render snapshot', () => {
    const clientList = create(
        <ClientList clients={[
            { displayName: "foo", id: "bar" },
            { displayName: "foo1", id: "bar1" }
            ]} />
    ).toJSON();
    expect(clientList).toMatchSnapshot();
});

it("MaterialForm render snapshot", () => {
    const materialForm = create(
        <MaterialForm action="Create sth" origin="../foo" onSubmit={(e) => e.preventDefault()} >
            <MaterialInput name="username"
                label="Username"
                onInputChange={new Function()}
                value="foo"
                placeholder="Username..."
                type="text" />
            <MaterialInput name="redirectUri"
                label="Redirect Uri"
                onInputChange={new Function()}
                value="http://bar.com"
                placeholder="http://example.com"
                type="text" />
        </MaterialForm>
        
    ).toJSON();
    expect(materialForm).toMatchSnapshot();
});
