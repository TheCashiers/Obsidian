import * as React from "react";
import {Motion, spring} from "react-motion";
import {NotificationState} from "../containers/NotificationCenterContainer";
import {styles} from "../styles/index";

const getClassName = (state: NotificationState) => {
    let ncStyle: string = "";
    switch (state) {
        case NotificationState.caution:
            ncStyle = "alert-warning";
            break;
        case NotificationState.error:
            ncStyle = "alert-danger";
            break;
        case NotificationState.info:
            ncStyle = "alert-info";
            break;
        case NotificationState.success:
            ncStyle = "alert-success";
            break;
        default:
            ncStyle = "alert-info";
    }
    return "alert " + ncStyle;
};
export const NotificationCenter = (props) => (
    <Motion
        style={{
        bottom: props.shouldShow
            ? spring((props.items.length * -60) + 60)
            : spring(props.items.length * -60),
        opacity: props.shouldShow
            ? spring(1)
            : spring(0),
        }}
    >
        {(style) =>
            (
                <div style={{ ...styles.notificationContainer, ...style }}>
                {props.items.map((item, index) =>
                        (
                            <NotificationItem
                                state={item.state}
                                info={item.info}
                                key={index}
                                index={index}
                                handleDismiss={props.handleDismiss}
                            />
                        ))}
                </div>
            )}
    </Motion>
);

export const NotificationItem = (props) => (
    <div
        style={styles.notification}
        className={getClassName(props.state)}
        onClick={props.handleDismiss}
    >
        <span>{props.info}</span>
    </div>
);
