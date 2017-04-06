import * as React from "react";
import { styles } from "../styles/index";
import { Motion, spring } from "react-motion";
import { NotificationState } from "../containers/NotificationCenterContainer";

const getClassName = (state:NotificationState) => {
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
    };
    return "alert "+ncStyle;
}
export const NotificationCenter = (props) =>(
    <div style={{ ...styles.notificationContainer, ...props.style }}>
        {props.items.map((item, index) =>
            <NotificationItem state={item.state} info={item.info} key={index} index={index} handleDismiss={props.handleDismiss}/>)}
</div>);
export const NotificationItem = (props) =>(
    <div style={styles.notification} className={getClassName(props.state)} onClick={(e) => props.handleDismiss(props.index)}>
        <span>{props.info}</span>
</div>);