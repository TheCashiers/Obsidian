import * as React from "react"
export const UserForm = (props) => (
    <form onSubmit={props.onSubmit}>
            Username: <input type="text" name="username" onChange={props.onInputChange} value={props.username}></input>
            Password: <input type="password" name="password" onChange={props.onInputChange} value={props.password}></input>
    <button className="btn btn-lg btn-success" type="submit">{props.action}</button>
    </form>
);