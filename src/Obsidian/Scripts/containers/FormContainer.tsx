import * as axios from "axios";
import * as React from "react";
import * as api from "../configs/GlobalSettings";

export abstract class FormContainer extends React.Component<any, any> {
    constructor(props) {
        if (!props.token) {
            throw new ReferenceError("Token must be fulfilled.");
        }
        super(props);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    public handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string,
        });
    }
}
