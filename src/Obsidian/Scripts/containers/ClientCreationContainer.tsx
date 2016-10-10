import * as React from "react";
import { CreateClient } from "../components/CreateClient";

export class ClientCreationContainer extends React.Component<any, any>
{
    constructor(props: any) {super(props);}

    render(){
        return <CreateClient/>
    }
}