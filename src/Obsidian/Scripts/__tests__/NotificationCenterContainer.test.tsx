import { mount, shallow } from "enzyme";
import * as React from "react";
import create from "react-test-renderer";
import { NotificationCenterContainer } from "../containers/NotificationCenterContainer";

test("dismiss function works correctly", () => {
    const mockSubmit = jest.fn();
    const notificationCenterContainer = mount(
        <NotificationCenterContainer/>);
});
