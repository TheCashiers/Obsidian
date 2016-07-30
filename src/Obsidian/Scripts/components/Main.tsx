// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { PortalHeader, PortalSidebar } from "./PortalElements"
var fixLayout = function () {
    //Get window height and the wrapper height
    var neg = $('.main-header').outerHeight() + $('.main-footer').outerHeight();
    var window_height = $(window).height();
    var sidebar_height = $(".sidebar").height();
    //Set the min-height of the content and sidebar based on the
    //the height of the document.
    if ($("body").hasClass("fixed")) {
        $(".content-wrapper, .right-side").css('min-height', window_height - $('.main-footer').outerHeight());
    } else {
        var postSetWidth;
        if (window_height >= sidebar_height) {
            $(".content-wrapper, .right-side").css('min-height', window_height - neg);
            postSetWidth = window_height - neg;
        } else {
            $(".content-wrapper, .right-side").css('min-height', sidebar_height);
            postSetWidth = sidebar_height;
        }

        //Fix for the control sidebar height
        var controlSidebar = $(".control-sidebar");
        if (typeof controlSidebar !== "undefined") {
            if (controlSidebar.height() > postSetWidth)
                $(".content-wrapper, .right-side").css('min-height', controlSidebar.height());
        }

    }
};
export let Main = React.createClass({
    componentDidUpdate: () => {
        fixLayout();
        console.log("re-rendered layout.")
    },
    render: function () {
        return (
            <div className="wrapper skin-blue">
                <PortalHeader/>
                <PortalSidebar/>
                {this.props.children}
            </div>)
    }
});
