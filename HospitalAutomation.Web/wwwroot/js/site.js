// Sidebar Toggle
document.addEventListener('DOMContentLoaded', function() {
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('sidebar');
    
    if (sidebarToggle && sidebar) {
        sidebarToggle.addEventListener('click', function() {
            sidebar.classList.toggle('collapsed');
            
            // Save state to localStorage
            const isCollapsed = sidebar.classList.contains('collapsed');
            localStorage.setItem('sidebarCollapsed', isCollapsed);
        });
        
        // Restore sidebar state from localStorage
        const savedState = localStorage.getItem('sidebarCollapsed');
        if (savedState === 'true') {
            sidebar.classList.add('collapsed');
        }
    }
    
    // Mobile sidebar toggle
    const mobileToggle = document.querySelector('.sidebar-toggle');
    if (window.innerWidth <= 768 && mobileToggle) {
        mobileToggle.addEventListener('click', function() {
            sidebar.classList.toggle('show');
        });
    }
    
    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function(event) {
        if (window.innerWidth <= 768 && sidebar && sidebar.classList.contains('show')) {
            if (!sidebar.contains(event.target) && !mobileToggle.contains(event.target)) {
                sidebar.classList.remove('show');
            }
        }
    });
    
    // Set active nav link based on current URL
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.sidebar-menu .nav-link');
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href && currentPath.includes(href.split('?')[0])) {
            link.classList.add('active');
        }
    });
});

// Smooth scroll for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});
