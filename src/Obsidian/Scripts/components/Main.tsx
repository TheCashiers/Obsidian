/// <reference path="../configs/global.d.ts" />
import * as React from "react";
import * as $ from "jquery"
import { PortalHeader } from "./PortalElements";
const fixLayout = function () {
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
    },
    render: function () {
        return (
            <div className="layout-top-nav wrapper skin-purple">
                <PortalHeader currentPath={this.props.route.path}/>
                {this.props.children}
            </div>)
    }
});
