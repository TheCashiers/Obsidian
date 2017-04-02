import * as React from "react";
import { styles } from "../styles/index";
import { NotificationState } from "../containers/PortalContainer";

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
    <div style={styles.notificationContainer}>
        {props.items.map((item, index) =>
            <NotificationItem state={item.state} info={item.info} key={index}/>)}
</div>);
export const NotificationItem = (props) =>(
    <div style={styles.notification} className={getClassName(props.state)}>
        <span>{props.info}</span>
</div>);