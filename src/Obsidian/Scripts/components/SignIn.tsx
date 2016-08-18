import * as React from "react"
export const SignIn = (props) => (
    <div className="main fullscreen">

    <div className="bg cover text-padding left-div">
        @*Left Section*@
        <h1>Obsidian</h1>
        <h3>One-stop Authenication Solution</h3>
    </div>
    <div className="white form form-padding">
        <form className="text-padding">
            @*Right Section*@
            <div className="form-group">
                <label htmlFor="UserName" className="control-label"></label>
                <input htmlFor="UserName" className="form-control" type="text" placeholder="Enter your E-mail address" required/>
            </div>

            <div className="form-group">
                <label className="control-label" htmlFor="Password"></label>
                <input className="form-control" htmlFor="Password" type="password" placeholder="Enter your password" required/>
            </div>

            <br/>

            <div className="form-inline">
                <input className="form-inline checkbox" htmlFor="RememberMe" type="checkbox" />
                <label className="control-label" htmlFor="RememberMe"></label>
            </div>

            <input htmlFor="ProtectedOAuthContext" type="hidden" />
            <br/>
            <hr/>
            <button className="btn btn-lg btn-info form-btn" type="submit">Sign In</button>
        </form>
    </div>
</div>
);