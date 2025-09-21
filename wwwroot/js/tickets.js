document.addEventListener("DOMContentLoaded", function () {
  document.querySelectorAll(".ticket-status").forEach(select => {
    select.addEventListener("change", async function (event) {
      event.preventDefault();
      const form = this.closest("form");
      const formData = new FormData(form);

      try {
        console.log("Form action", form.action)
        const response = await fetch(form.action, {
          method: "POST",
          body: formData
        });
        if (!response.ok) throw new Error("Update failed");

        const data = await response.json();
        console.log("Updated:", data);

        // Optional: show user feedback
        this.style.border = "2px solid green";
        setTimeout(() => this.style.border = "", 1000);
      } catch (err) {
        console.error(err);
        this.style.border = "2px solid red";
      }
    });
  });
})

