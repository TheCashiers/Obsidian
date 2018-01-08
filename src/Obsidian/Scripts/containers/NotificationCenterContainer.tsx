import * as React from "react";
import { Motion, spring } from "react-motion";
import { NotificationCenter } from "../components/Notification";

const NOTIFICATION_DISPLAY_TIME = 3000;

interface INotificationCenterState {
    notifications: INotification[];
    shouldShow: boolean;
}
export class NotificationCenterContainer extends React.Component<{}, INotificationCenterState> {
    private timer: number;
    constructor() {
        super();
        this.state = { notifications: [] as INotification[], shouldShow: false };
        this.pushNotification = this.pushNotification.bind(this);
        this.handleDismiss = this.handleDismiss.bind(this);
    }
    public handleDismiss() {
        this.setState({
            shouldShow: false,
        });
    }
    public pushNotification(desc: string, error?: string) {
        if (typeof (error) === "string") {
            const nextNc = { info: `${desc} failed. ${error}.`, state: NotificationState.error };
            this.setState({
                notifications: [nextNc].concat(this.state.notifications as INotification[]),
            });
        } else {
            const nextNc = { info: `${desc} successfully.`, state: NotificationState.success };
            this.setState({
                notifications: [nextNc].concat(this.state.notifications as INotification[]),
            });
        }
        this.setState({ shouldShow: true });
        clearTimeout(this.timer);
        this.timer = setTimeout(function() {
            this.setState({ shouldShow: false });
        }.bind(this), NOTIFICATION_DISPLAY_TIME);
    }

    public render() {
        return (
            <NotificationCenter
                shouldShow={this.state.shouldShow}
                items={this.state.notifications}
                handleDismiss={this.handleDismiss}
            />
        );

    }
}

export interface INotification {
    info: string;
    state: NotificationState;
}

export enum NotificationState {
    info,
    error,
    caution,
    success,
}
