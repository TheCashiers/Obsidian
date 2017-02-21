import * as React from "react";
import { PortalHeader } from "./PortalElements";

export let Portal = (props)=>(
    <div className="layout-top-nav wrapper skin-purple">
                <PortalHeader/>
                {React.cloneElement(props.children, { token: props.token })}
    </div>
)