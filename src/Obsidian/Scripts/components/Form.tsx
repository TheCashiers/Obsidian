import * as React from "react";
import { Link } from "react-router";
import { styles } from "../styles";
export const MaterialForm = (props) => (
    <div className="well bs-component col-md-6">
        <form className="form-horizontal" onSubmit={props.onSubmit}>
            <fieldset>
                <legend>{props.action}</legend>
            </fieldset>
            {props.children}
            <div className="form-group">
                <div className="col-md-10 col-md-offset-2">
                    <Link to={props.origin}>
                        <button type="button" className="btn btn-default">Cancel</button>
                    </Link>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </div>
        </form>
    </div>
)

export const Input = (props) => (
    <div className="form-group">
        <label className="col-md-2 control-label">{props.label}</label>
        <div className="col-md-10">
            <input type={props.type} className="form-control"
                name={props.name}
                onChange={props.onInputChange}
                value={props.value}
                placeholder={props.placeholder}
                />
        </div>
    </div>
)
export const UserForm = (props) => (
    <MaterialForm action={props.action}
        origin="/manage/users"
        onSubmit={props.onSubmit}>
        <Input name="username"
            label="Username"
            onInputChange={props.onInputChange}
            value={props.username}
            placeholder="Username..."
            type="text" />
        <Input name="password"
            label="Password"
            onInputChange={props.onInputChange}
            value={props.password}
            placeholder="Password..."
            type="password" />
    </MaterialForm>
);

export const ScopeForm = (props) => (
    <MaterialForm action={props.action}
        origin="/manage/scopes"
        onSubmit={props.onSubmit}>
    <Input name="scopeName"
            label="Scope Name"
            onInputChange={props.onInputChange}
            value={props.scopeName}
            placeholder="Scope Name..."
            type="text" />
    <Input name="displayName"
            label="Display Name"
            onInputChange={props.onInputChange}
            value={props.displayName}
            placeholder="Display Name..."
            type="text" />
    <Input name="description"
            label="Description"
            onInputChange={props.onInputChange}
            value={props.description}
            placeholder="Description..."
            type="text" />
     <Input name="claimTypes"
            label="Claim Types"
            onInputChange={props.onInputChange}
            value={props.claimTypes}
            placeholder="Claim Types..."
            type="text" />       
    </MaterialForm>
);

export const ClientForm = (props)=>(
    <MaterialForm action={props.action}
        origin="/manage/clients"
        onSubmit={props.onSubmit}>
        <Input name="displayName"
            label="Display Name"
            onInputChange={props.onInputChange}
            value={props.displayName}
            placeholder="Display Name..."
            type="text" />
        <Input name="redirectUri"
            label="Redirect Uri"
            onInputChange={props.onInputChange}
            value={props.redirectUri}
            placeholder="http://example.com"
            type="text" />
    </MaterialForm>
);