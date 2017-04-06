import * as React from "react";
import { PortalHeader } from "./PortalElements";
import { NotificationCenterContainer } from "../containers/NotificationCenterContainer";

export let Portal = (props)=>(
    <div className="layout-top-nav wrapper skin-purple">
        {props.children}
        
    </div>
)   