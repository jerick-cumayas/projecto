document.addEventListener("DOMContentLoaded", function () {
  // 1) Wait until the HTML is parsed and available

  const sprintsTab = document.getElementById("sprints-tab")
  if (!sprintsTab) return;

  sprintsTab.addEventListener("shown.bs.tab", function () {
    const container = document.getElementById("sprints-content")
    if (!container) return;

    if (container.dataset.loaded) return;

    const projectId = container.dataset.projectId;

    fetch(`/Sprints/ListByProject?projectId=${projectId}`).then(response => response.text()).then(html => {
      container.innerHTML = html;
      container.dataset.loaded = "true";
    }).catch(err => {
      // 8) Graceful error state + log for debugging
      container.innerHTML = `<p class="text-danger">Failed to load sprints.</p>`;
      console.error(err);
    })
  });

  const ticketsTab = document.getElementById("tickets-tab");
  if (!ticketsTab) return; // guard if this view doesn’t have the tab

  // 2) Listen for Bootstrap's "tab became active" event
  ticketsTab.addEventListener("shown.bs.tab", function () {
    const container = document.getElementById("tickets-content");
    if (!container) return;

    // 3) Avoid fetching twice (like React's useEffect with [])
    if (container.dataset.loaded) return;

    // 4) Read the project id from the DOM (data-project-id="123")
    const projectId = container.dataset.projectId;

    // 5) Fetch the server-rendered partial HTML for just this project’s tickets
    fetch(`/Tickets/ListByProject?projectId=${projectId}`)
      .then(response => response.text())  // convert HTTP response to HTML string
      .then(html => {
        // 6) Inject the HTML into the tab's content area
        container.innerHTML = html;

        // 7) Mark as loaded so we don’t refetch on later tab switches
        container.dataset.loaded = "true";
      })
      .catch(err => {
        // 8) Graceful error state + log for debugging
        container.innerHTML = `<p class="text-danger">Failed to load tickets.</p>`;
        console.error(err);
      });
  });
});