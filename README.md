# Blackbird.io Phrase Strings

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

[Phrase Strings](https://phrase.com/platform/strings/) is a translation management platform for software projects. You can collaborate on language file translation with your team or order translations through our platform. The API allows you to import locale files, download locale files, tag keys or interact in other ways with the localization data stored in Phrase Strings for your account.

## Before setting up

Before you can connect you need to make sure that:

- You have a Phrase Strings account on the instance you want to connect to.
- In Phrase Strings go to Settings > Profile > Access tokens
- Push the _Generate token_ button.
- Create a new access token with the scope: 'read', 'write', 'orders.create', 'team.manage'.
- Copy the generated access token to your clipboard. You will need it to connect to Phrase Strings from Blackbird.io.

## Connecting

1. Navigate to apps and search for Phrase.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My Phrase connection'.
4. Fill in the _Access token_ that you copied from Phrase String.
5. Select the data center your Phrase instance is hosted on. You can select between the US and EU data centers.
6. Click _Authorize connection_.


## Actions

### Job

- **Search jobs** Searches jobs according to the filters you specify.
- **Create Job**  Creates a new job.
- ** Get Job** Retrieves a job by its ID.
- **Start job** Starts a job.
- ** Add keys to job** Adds keys to a job.
- **Complete job** Completes a job.
- **Reopen job** Reopens a job.

### Comment

- **Add comment to a key** Adds a comment to a key.
- **Add comment to a job** Adds a comment to a job.

### Key

-**Search keys** Searches keys according to the filters you specify.
- **Create a key** Creates a new key.
- **Update a key** Updates an existing key.
- **Add tags to a key** Adds tags to a key.
- **Remove tags from a key** Removes tags from a key

### Order

- **Seach orders** Searches orders according to the filters you specify.
- **Create order** Creates a new order.
- **Get order** Retrieves an order by its ID.
- **Confirm order** Confirms an order.

### Project

- **Search projects** Searches projects according to the filters you specify.
- **Get project** Retrieves a project by its ID.
- **Delete project** Deletes a project by its ID.
- **Create project** Creates a new project.
- **Update project** Updates an existing project.

### Repository

- **Export to code repository** Exports a project to a code repository.
- **Import from code repository** Imports a project from a code repository.

### Translation

- **Create translation** Creates a new translation.
- **Update translation** Updates an existing translation.
- **Get translation for a key** Retrieves a translation for a key.
- **Get translation for a locale** Retrieves a translation for a locale.
- **Download locale** Downloads a locale file.
- **Upload file** Uploads a file to the project.

## Events

### Job

- **On job completed** Triggered when a job is completed.

### Comment

- **On comment added to a key** Triggered when a comment is added to a key.

### Key

- **On key created** Triggered when a key is created.
- **On key updated** Triggered when a key is updated.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
