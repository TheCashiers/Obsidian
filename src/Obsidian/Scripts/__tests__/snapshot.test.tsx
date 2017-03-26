import * as React from "react";
import { ClientList } from "../components/ClientManagement"
import { MaterialForm } from "../components/Form"
import { create } from 'react-test-renderer';

it('ClientList render snapshot', () => {
    const clientList = create(
        <ClientList clients={[{ displayName:"foo",id:"bar"}]} />
    ).toJSON();
    expect(clientList).toMatchSnapshot();
});

it("MaterialForm render snapshot", () => {
    const materialForm = create(
        <MaterialForm action="Create sth" origin="../foo" onSubmit={(e)=>e.preventDefault()}/>
    ).toJSON();
    expect(materialForm).toMatchSnapshot();
})
