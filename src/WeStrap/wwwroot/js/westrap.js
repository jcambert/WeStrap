window.westrap = {
    sidebar_click: function (elt, id) {
        // console.log("Sidebar clicked", elt);
        let $li = $("#" + id);
        if ($li.length == 0) return;
        console.log(id);
        let $SIDEBAR_MENU = $li.closest("#sidebar-menu");
        let $current_active = $SIDEBAR_MENU.find('li.active');

        function slideUp(elt) {
            if (elt.length == 0) return;
            elt.removeClass('active');
            elt.find('a>span').removeClass('fa-chevron-up').addClass('fa-chevron-down');
            $('ul:first', elt).slideUp();
        }
        function slideDown(elt) {
            if (elt.length == 0) return;
            elt.addClass('active');
            elt.find('a>span').removeClass('fa-chevron-down').addClass('fa-chevron-up');
            elt.children('ul:first').slideDown();
        }
        if ($current_active.attr('id') == id) {
            slideUp($current_active);
        } else {
            slideUp($current_active);
            slideDown($li)
        }


    },
    show_picker: function (pickId) {
        console.log(pickId);
        let sel = document.getElementById(pickId);
        if (sel._jscLinkedInstance)
            sel._jscLinkedInstance.show();
        else {
            let picker = new jscolor(sel);
            picker.show();
        }
    },
    update_picker: function (pickId, id, color) {
        let sel = document.getElementById(pickId);
        let _color = color;
        console.log("set color:", _color)
        if (!sel._jscLinkedInstance) {
            new jscolor(sel);
            console.log('create new jscolor');
        }
        if (_color !== undefined) {
            var result = sel._jscLinkedInstance.fromString(_color.replace("#",""));
            console.log("update init color", _color, result);
        }

        _color = sel._jscLinkedInstance.toRGBString();
        console.log("the current color",_color)
        document.getElementById(id).style.backgroundColor = _color
        console.log("Update picker");
        return _color;
    }
}