import * as React from "react";
import { NotificationCenter } from "../components/Notification";
import { Motion, spring } from "react-motion";

const NOTIFICATION_DISPLAY_TIME = 3000;
export class NotificationCenterContainer extends React.Component<any, any>{
    private _timer: number;
    constructor(props) {
        super(props);
        this.state = { notifications: [] as Notification[], shouldShow: false };
        this.pushNotification = this.pushNotification.bind(this);
        this.handleDismiss = this.handleDismiss.bind(this);
    }
    public handleDismiss(index:number) {
        this.setState({
            shouldShow: false
        });
    }
    public pushNotification(desc: string, error?: string) {
        if (typeof (error) === 'string') {
            let nextNc = { info: `${desc} failed. ${error}.`, state: NotificationState.error };
            this.setState({
                notifications: (this.state.notifications as Array<Notification>).concat([nextNc])
            });
        }
        else {
            let nextNc = { info: `${desc} successfully.`, state: NotificationState.success };
            this.setState({
                notifications: (this.state.notifications as Array<Notification>).concat([nextNc])
            });
        }
        this.setState({ shouldShow: true });
        clearTimeout(this._timer);
        this._timer = setTimeout(function () {
            this.setState({ shouldShow: false });
        }.bind(this), NOTIFICATION_DISPLAY_TIME);
    }

    render() {
        return <NotificationCenter shouldShow={this.state.shouldShow} items={this.state.notifications} handleDismiss={this.handleDismiss}/>
            
    }
}

interface Notification {
    info: string;
    state: NotificationState
}


export enum NotificationState {
    info,
    error,
    caution,
    success
}

