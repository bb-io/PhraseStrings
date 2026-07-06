# Blackbird.io Phrase Strings

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

[Phrase Strings](https://phrase.com/platform/strings/) is a translation management platform for software projects. This app lets you automate project, key, translation, job, order, screenshot, repository sync, user, team, comment, and file workflows from Blackbird.

## Before setting up

Before you can connect, make sure that:

- You have a Phrase Strings account on the instance you want to connect to.
- In Phrase Strings, go to Settings > Profile > Access tokens.
- Click the _Generate token_ button.
- Select _Phrase Strings_ and name your token for future reference.
- Copy the generated access token to your clipboard. You will need it to connect to Phrase Strings from Blackbird.io.

## Connecting

> **Note**: The 'Platform API token' connection type is recommended for all new connections.

1. Navigate to apps and search for Phrase.
2. Click _Add Connection_.
3. Name your connection for future reference, e.g. 'My Phrase connection'.
4. Fill in the _Access token_ that you copied from Phrase Strings.
5. Select the base URL for your data center. You can select the EU, US, or EU staging data center.
6. Click _Authorize connection_.

## Actions

### Jobs

- **Search jobs** Search jobs that match selected filters.
    Advanced settings:
    - **Branch**: Limit the search to a branch.
    - **Owned by**: Limit results to jobs owned by a user.
    - **Assigned to**: Limit results to jobs assigned to a user.
    - **Job name contains**: Search jobs by partial name.
    - **State**: Limit results to a job state.
    - **Updated since**: Limit results to jobs updated after this date.
- **Create job** Create a job. Target locales can be provided as locale IDs or locale codes.
    Advanced settings:
    - **Source locale ID**: Set the source locale.
    - **Briefing**: Add job instructions.
    - **Due date**: Set a due date.
    - **Ticket URL**: Add an external ticket URL.
    - **Tags**: Add job tags.
    - **Translation key IDs**: Add keys by ID.
    - **Target locale IDs**: Add target locales by ID.
    - **Target locale codes**: Add target locales by code.
    - **Job template ID**: Use a job template.
- **Get job** Get a job by its ID.
- **Start job** Start a job and output job details.
- **Update job** Update job details.
    Advanced settings:
    - **Job name**: Set a new job name.
- **Add keys to job** Add keys to a job. Keys can be provided as key IDs or key names.
    Advanced settings:
    - **Key IDs**: Add keys by ID.
    - **Key names**: Add keys by name.
- **Add target locales to a job** Add target locales to a job. Use **Get project locales** to obtain locale IDs from locale codes.
    Advanced settings:
    - **Branch name**: Add target locales on a branch.
    - **Translator IDs to be assigned**: Assign translator users.
    - **Translator team IDs to be assigned**: Assign translator teams.
    - **Reviewer IDs to be assigned**: Assign reviewer users.
    - **Reviewer team IDs to be assigned**: Assign reviewer teams.
- **Complete a job** Complete a job and output job details.
- **Reopen a job** Reopen a job and output job details.

### Comments

- **Add comment to a key** Add a comment to a key.
    Advanced settings:
    - **Message**: Set the comment text.
    - **Locales**: Link the comment to locales.
- **Add comment to a job** Add a comment to a job.

### Screenshots

- **Upload screenshot** Upload a screenshot.
    Advanced settings:
    - **Description**: Add screenshot details.
- **Mark screenshot (link to key)** Connect a key to a screenshot so the screenshot appears in the editor.
    Advanced settings:
    - **Presentation details**: Add marker presentation details.
- **Get uploaded screenshot** Get a screenshot by its ID or name.
    Advanced settings:
    - **Screenshot ID**: Find a screenshot by ID.
    - **Screenshot name**: Find a screenshot by name.

### Figma

- **Add Figma link to key** Add a Figma link to a key.

### Keys

- **Search keys** Search keys that match selected filters.
    Advanced settings:
    - **Sort**: Select the sort field.
    - **Order**: Select the sort order.
    - **Locale ID**: Search keys for a locale.
- **Get key by name** Get key details by key name. Useful for retrieving a key ID or key tags.
- **Create a key** Create a key.
    Advanced settings:
    - **Name**: Set the key name.
    - **Plural**: Mark the key as plural.
    - **Plural name**: Set the plural key name.
    - **Data type**: Set the key data type.
    - **Tags (comma-separated)**: Add tags as comma-separated text.
    - **Max characters allowed**: Set the key character limit.
    - **Unformatted**: Mark the key as unformatted.
    - **Default translation content**: Set default translation content.
    - **Autotranslate**: Enable automatic translation.
    - **XML space preserve**: Preserve XML space.
    - **Original file**: Set the original file path.
    - **Localized format string**: Set the localized format string.
    - **Localized format key**: Set the localized format key.
- **Update a key** Update a key.
- **Add tags to keys** Add tags to matching keys.
    Advanced settings:
    - **Query**: Filter keys by search query.
- **Remove tags from keys** Remove tags from matching keys.
- **Link keys to parent key** Link one or more child keys to a parent key and output linked key IDs.

### Interoperable XLIFF

- **Download keys** Download selected keys for downstream translation or review.
    Advanced settings:
    - **Source locale ISO code or ID**: Override the source locale.
    - **Job ID**: Download keys from a job.
- **Upload keys** Upload translated key content and update target translations.
    Advanced settings:
    - **Target states to apply**: Choose which translated segments should update Phrase review status.
    - **Add tags to uploaded keys**: Add tags to affected keys.
    - **Target locale ID**: Override the target locale ID from file metadata.
    - **Project ID**: Override the project ID from file metadata.

**Download keys** outputs the content file, locale IDs, total downloaded keys, keys with no target text, keys that require review, verified keys, and reviewed keys.

Round trip flow:

1. Use **Download keys** with a project, target locale, and optional source locale, job, key IDs, or key names.
2. Send the returned XLIFF file through downstream translation or review steps.
3. Use **Upload keys** with the same XLIFF file. Project ID, source locale ID, target locale ID, branch, and job ID are read from file metadata. Key IDs are read from unit IDs, and translation IDs are read from segment IDs. Optional upload inputs can override project ID, target locale ID, and branch.

The XLIFF stores one Phrase key per unit and one segment per unit. The file keeps Phrase IDs and metadata so **Upload keys** can update the same project, locales, branch, job, keys, and existing translations. Key descriptions are written as notes, and key character limits are kept as XLIFF size restrictions.

Downloaded segments use these review statuses:

- No target text means the segment is still empty (state `initial`).
- Target text that still needs checking is translated (state `translated`).
- Verified target text is ready for normal use (state `reviewed`).
- Reviewed target text is finalized and is also verified (state `final`).

On upload, selected segment statuses update Phrase review status:

- `Reviwed` segments become verified in Phrase.
- `Final` segments become reviewed and verified in Phrase.

### Orders

- **Search orders** Search orders that match selected filters.
- **Create order** Create a translation order.
    Advanced settings:
    - **Target locale IDs**: Select target locales.
    - **Translation type**: Select the translation type.
    - **Tag**: Set the order tag.
    - **Styleguide ID**: Set the styleguide.
    - **Unverify translations upon delivery**: Mark delivered translations as unverified.
    - **Include untranslated keys**: Include untranslated keys in the order.
    - **Include unverified translations**: Include unverified translations in the order.
    - **Quality**: Request quality review.
    - **Priority**: Mark the order as priority.
- **Get order** Get an order by its ID.
- **Confirm order** Confirm an order.

### Projects

- **Search projects** Search projects that match selected filters, including case-insensitive project name matching.
    Advanced settings:
    - **Account ID**: Search projects in an account.
    - **Project name contains**: Search projects by partial name.
- **Get project** Get a project by its ID.
- **Get project from link** Get a project from its web link.
- **Delete project** Delete a project by its ID.
- **Create project** Create a project.
    Advanced settings:
    - **Main format**: Set the main file format.
    - **Media**: Set the project media.
    - **Shares translation memory**: Share translation memory.
    - **Remove project image**: Remove the project image.
    - **Point of contact**: Set the point of contact.
    - **Source project ID**: Create from another project.
    - **Workflow**: Set the workflow.
    - **Machine translation enabled**: Enable machine translation.
    - **Enable branching**: Enable branches.
    - **Protect master branch**: Protect the master branch.
    - **Enable all data type translation keys for translators**: Allow translators to access all data type translation keys.
    - **Enable ICU message format**: Enable ICU message format.
    - **Zero plural form enabled**: Enable zero plural forms.
    - **Autotranslate enabled**: Enable autotranslation.
    - **Autotranslate check new translation keys**: Autotranslate new keys.
    - **Autotranslate check new uploads**: Autotranslate new uploads.
    - **Autotranslate check new locales**: Autotranslate new locales.
    - **Autotranslate mark as unverified**: Mark autotranslations as unverified.
    - **Autotranslate use machine translation**: Use machine translation for autotranslation.
    - **Autotranslate use translation memory**: Use translation memory for autotranslation.
    - **File**: Upload a project image file.
- **Update project** Update a project.
- **Get project locales** Get locales for a project, useful for obtaining locale IDs.
- **Get project locale from code** Get a project locale by ISO code, useful for obtaining locale IDs.

### Project variables

- **Get variable value** Get a project variable value.
- **Set variable** Create or update a project variable. If the variable exists, it is updated; if it does not exist, it is created.

Both actions output the variable value, creation time, and last update time.

### Repositories

- **Search repositories** Search repository syncs for an account.
    Advanced settings:
    - **Ignore inactive repository syncs**: Do not include inactive repository syncs.
- **Find repository** Find the first repository sync by project ID or repository name.
    Advanced settings:
    - **Repository name**: Find by repository name.
- **Export to code repository** Export project content to a code repository. Add a delay between exports if you're starting multiple exports in a row to avoid rate limiting issues.
- **Import from code repository** Import project content from a code repository.

### Translations

- **Create translation** Create a translation.
    Advanced settings:
    - **Key ID**: Select the key.
    - **Plural suffix**: Set the plural suffix.
    - **Unverified**: Mark the translation as unverified.
    - **Excluded**: Exclude the translation.
- **Update translation** Update a translation.
    Advanced settings:
    - **Translation ID**: Select the translation.
- **Get translations for key** Get translations for a key.
- **Get translations for locale** Get translations for a locale.
- **Download locale** Download locale content.
    Advanced settings:
    - **Include empty translations**: Include empty translations.
    - **Exclude empty zero forms**: Exclude empty zero plural forms.
    - **Include translated keys**: Include translated keys.
    - **Keep 'notranslate' tags**: Keep `notranslate` tags.
    - **Encoding**: Set file encoding.
    - **Include unverified translations**: Include unverified translations.
    - **Use last reviewed version**: Use last reviewed translations.
    - **Fallback locale ID**: Set fallback locale.
    - **Translation key prefix**: Filter by translation key prefix.
    - **Filter by prefix**: Treat the prefix as a filter.
    - **Locale IDs**: Include specific locales.
- **Upload file** Upload a localization file into a project.
    Advanced settings:
    - **Update translations**: Update existing translations.
    - **Update translation keys**: Update existing keys.
    - **Update translations on source match**: Update translations when source content matches.
    - **Update descriptions**: Update key descriptions.
    - **Skip upload tags**: Do not add upload tags.
    - **Skip unverification**: Do not unverify updated translations.
    - **File encoding**: Set upload file encoding.
    - **Verify mentioned translations**: Verify translations mentioned in the upload.
    - **Mark reviewed**: Mark uploaded translations as reviewed.
    - **Tag only affected keys**: Tag only keys affected by the upload.

### Users and teams

- **Get user by email** Get user details by email address, useful for obtaining user ID.
- **Get team by name** Get team details by team name, useful for obtaining team ID.
    Advanced settings:
    - **Use partial name match**: Match teams by partial name instead of exact name.

## Events

### Job

- **On job completed** Runs when a job is completed.
- **On phrase job status change** Runs when a job is created, activated, or completed.
    Advanced settings:
    - **Events to react to**: Select job status events.
    - **Project IDs**: Select projects to watch.
    - **Job IDs**: Filter by jobs.
    - **Job name contains**: Filter by partial job name.
    - **Job name doesn't contain**: Exclude jobs by partial name.

### Comment

- **On comment added to a key** Runs when a comment is added to a key.

### Key

- **On key created** Runs when a key is created.
- **On key updated** Runs when a key is updated.

### Repository

- **On repository sync failure** Runs when a repository sync import or export fails.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
