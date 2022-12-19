/**
 * Thu gọn sidebar
 * author: NDTRUNG (18/10/2022)
 */
function toggleSidebar() {
    $(".toggle-sidebar .m-icon--menu").click(function() {
        // toggle logo
        $(".header__left").toggle();
        $(this).toggleClass("m-icon--menu-collapse");
        $(this).parent().toggleClass("collapse-sidebar");

        // toggle sidebar
        $(".sidebar").toggleClass("sidebar--collapse");
        $(".sidebar .sidebar-text").fadeToggle();
    });
}