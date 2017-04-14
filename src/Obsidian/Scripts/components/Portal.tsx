import * as React from "react";
import { PortalHeader } from "./PortalElements";
import { NotificationCenterContainer } from "../containers/NotificationCenterContainer";
import { Motion, spring } from "react-motion";

export let Portal = (props) => (
    <Motion defaultStyle={{ opacity: 0 }} style={{ opacity: spring(1) }}>
        {(style) =>
            <div style={style} className="layout-top-nav wrapper skin-purple">
                {props.children}

            </div>}
    </Motion>
    
)   