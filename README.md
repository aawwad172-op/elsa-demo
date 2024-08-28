# Elsa

### What is workflow?

    A series of connected steps (activities) that are executed in sequence. It tracks the current activity, variable, and any blocking activities.

### What is Activity?

    A single step in a workflow. It performs a specific action and can have different outcomes.

### What is Starting Activity?

    The entry point of a workflow, with no inbound connections.

### What is Blocking Activity?

    An activity that pauses the workflow until a specific condition or event triggers its continuation

### What is Suspended Workflow?

    A workflow paused by a blocking activity, waiting for a trigger to resume.

### What is Connection?

    A link between two activities that defines the flow based on outcomes (e.g., success, failure).

### What is Long-Running Workflow?

    A workflow that pauses at certain points, waiting for external triggers before continuing.

### What is Short-Running Workflow?

    A workflow that runs continuously from start to finish without pauses.

### What is Burst of Execution?

    A rapid execution of multiple activities until the workflow either ends or encounters a blocking activity.
