# Call Detail Records (CDR) API

This API provides endpoints for managing call detail records (CDRs) including uploading CDR files, retrieving statistics, and accessing call details.

## Endpoints

1. **Upload CDR File**
   - **Description:** Upload a CSV file containing call detail records (CDRs) to store them in the database.
   - **Method:** POST
   - **Endpoint:** `/api/seed`
   - **Parameters:**
     - `filePath`: Path to the CSV file to upload.
   - **Response:** Returns a success message if the file is uploaded successfully.

2. **Retrieve Call Cost Statistics**
   - **Description:** Retrieve statistics such as average call cost, maximum call cost, and minimum call cost.
   - **Method:** GET
   - **Endpoint:** `/api/record/call-cost-statistics`
   - **Response:** Returns statistics for call costs.

3. **Retrieve Call Duration Statistics**
   - **Description:** Retrieve statistics such as average call duration, longest call duration, and shortest call duration.
   - **Method:** GET
   - **Endpoint:** `/api/record/call-duration-statistics`
   - **Response:** Returns statistics for call durations.

4. **Retrieve Call Count Statistics**
   - **Description:** Retrieve statistics such as total number of calls and average number of calls per day.
   - **Method:** GET
   - **Endpoint:** `/api/record/call-count-statistics`
   - **Response:** Returns statistics for call counts.

5. **Retrieve All Records**
   - **Description:** Retrieve all call detail records (CDRs) from the database.
   - **Method:** GET
   - **Endpoint:** `/api/record`
   - **Response:** Returns all call detail records stored in the database.

6. **Retrieve Call Details**
   - **Description:** Retrieve detailed information about specific calls based on filters like caller ID and recipient.
   - **Method:** GET
   - **Endpoint:** `/api/record/call-details`
   - **Parameters:**
     - `callerId`: Caller ID filter (optional).
     - `recipient`: Recipient filter (optional).
   - **Response:** Returns detailed call information based on the provided filters.

## Usage
- Use the provided Postman collection to interact with the API endpoints.
