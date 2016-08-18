import * as React from "react"
export const SignIn = (props) => (
    <div className="main fullscreen">

    <div className="bg cover text-padding left-div">
        <h1>Obsidian</h1>
        <h3>One-stop Authenication Solution</h3>
    </div>
    <div className="white form form-padding">
        <form className="text-padding">
            <div className="form-group">
                <label htmlFor="UserName" className="control-label">Username: </label>
                <input id="UserName" className="form-control" type="text" placeholder="Enter your E-mail address" required/>
            </div>

            <div className="form-group">
                <label className="control-label" htmlFor="Password">Password: </label>
                <input className="form-control" id="Password" type="password" placeholder="Enter your password" required/>
            </div>

            <br/>

            <div className="form-inline">
                <input className="form-inline checkbox" id="RememberMe" type="checkbox" />
                <label className="control-label" htmlFor="RememberMe">Remember Me</label>
            </div>

            <input htmlFor="ProtectedOAuthContext" type="hidden" />
            <br/>
            <hr/>
            <button className="btn btn-lg btn-info form-btn" type="submit">Sign In</button>
        </form>
    </div>
</div>
);