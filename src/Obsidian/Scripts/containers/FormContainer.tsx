import * as axios from "axios";
import * as React from "react";
import * as api from "../configs/GlobalSettings";

export interface IFormProps {
  token: string;
  location?: {
      query: {
          id: string;
          username: string;
      };
  };
  filter?: string;
  push?(reason: string, error?: string): void;
}

export interface IFormState {
  id?: string;
  username?: string;
  password?: string;
  claims?: string;
  description?: string;
  displayName?: string;
  scopeName?: string;
  claimTypes?: string;
  redirectUri?: string;
  emailAddress?: string;
  gender?: string;
  givenName?: string;
  surnName?: string;
  editing?: boolean;
}

interface ITarget {
    name: string;
    value: string;
}

export interface INamedEvent {
    target: ITarget;
}
export abstract class FormContainer extends React.Component<IFormProps, IFormState> {
  constructor(props: IFormProps) {
    if (!props.token) {
      throw new ReferenceError("Token must be fulfilled.");
    }
    super(props);
    this.handleInputChange = this.handleInputChange.bind(this);
  }

  public handleInputChange(e: INamedEvent) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }
}
