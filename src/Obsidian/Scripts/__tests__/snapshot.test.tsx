import * as React from "react";
import { create } from "react-test-renderer";
import { ClientList } from "../components/ClientManagement";
import { MaterialForm, MaterialInput } from "../components/Form";
import { List } from "../components/List";
import { NotificationCenter } from "../components/Notification";
import { PortalHeader, UserInfo } from "../components/PortalElements";
import { PortalIndex } from "../components/PortalIndex";
import { ScopeList } from "../components/ScopeManagement";
import { UserList } from "../components/UserManagement";
import { INamedEvent } from "../containers/FormContainer";

const mockChange = jest.fn<INamedEvent>();
const mockSubmit = jest.fn();
const mockDismiss = jest.fn();

it("List render snapshot", () => {
    const list = create(
        <List
            items={[
            { userName: "foo", id: "bar" },
            { displayName: "foo1", id: "bar1" },
            { scopeName: "foo2", id: "bar2" },
            ]}
        />,
    ).toJSON();
    expect(list).toMatchSnapshot();
});

it("ClientList render snapshot", () => {
    const clientList = create(
        <ClientList
            clients={[
            { displayName: "foo", id: "bar" },
            { displayName: "foo1", id: "bar1" },
            ]}
        />,
    ).toJSON();
    expect(clientList).toMatchSnapshot();
});

it("MaterialForm render snapshot", () => {
    const materialForm = create(
        <MaterialForm action="Create sth" origin="../foo" onSubmit={mockSubmit}>
            <MaterialInput
                name="username"
                label="Username"
                onInputChange={mockChange}
                value="foo"
                placeholder="Username..."
                type="text"
            />
            <MaterialInput
                name="redirectUri"
                label="Redirect Uri"
                onInputChange={mockChange}
                value="http://bar.com"
                placeholder="http://example.com"
                type="text"
            />
        </MaterialForm>,
    ).toJSON();
    expect(materialForm).toMatchSnapshot();
});

it("PortalHeader render snapshot", () => {
    const portalHeader = create(
        <PortalHeader token="MockedToken" filter="" handleFilterChange={mockChange} />,
    ).toJSON();
    expect(portalHeader).toMatchSnapshot();
});

it("PortalIndex render snapshot", () => {
    const portalIndex = create(
        <PortalIndex />,
    ).toJSON();
    expect(portalIndex).toMatchSnapshot();
});

it("ScopeList render snapshot", () => {
    const scopeList = create(
        <ScopeList
            scopes={[
            { displayName: "foo", id: "bar" },
            { displayName: "foo1", id: "bar1" },
            ]}
        />,
    ).toJSON();
    expect(scopeList).toMatchSnapshot();
});

it("UserList render snapshot", () => {
    const userList = create(
        <UserList
            users={[
            { displayName: "foo", id: "bar" },
            { displayName: "foo1", id: "bar1" },
            ]}
        />,
    ).toJSON();
    expect(userList).toMatchSnapshot();
});

it("Notifications render snapshot", () => {
    const notification = create(
        <NotificationCenter
            items={[
            { info: "foo", state: 1 },
            { info: "bar", state: 2 },
            ]}
            shouldShow={true}
            handleDismiss={mockDismiss}
        />,
    ).toJSON();
    expect(notification).toMatchSnapshot();
});
