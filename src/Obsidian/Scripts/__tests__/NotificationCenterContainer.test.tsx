import create from "react-test-renderer";
import * as React from 'react';
import { shallow, mount } from 'enzyme';
import { NotificationCenterContainer } from '../containers/NotificationCenterContainer';

test("dismiss function works correctly", () => {
    let mockSubmit = jest.fn();
    let notificationCenterContainer = mount(<NotificationCenterContainer items={[
        { info: "foo", state: 1 },
        { info: "bar", state: 2 }
    ]} />);
});