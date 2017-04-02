import * as React from "react";
import { PortalHeader } from "./PortalElements";
import { NotificationCenter } from "./Notification";

export let Portal = (props)=>(
    <div className="layout-top-nav wrapper skin-purple">
        
        <PortalHeader token={props.token}/>
        {React.cloneElement(props.children, { token: props.token, push: props.push })}
        <NotificationCenter items={props.notifications}/>
    </div>
)