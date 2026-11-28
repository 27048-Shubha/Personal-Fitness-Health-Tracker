# Personal Fitness & Health Tracker

A fitness and health tracking C# console application that helps users monitor daily wellness metrics, maintain healthy habits, and track long-term progress through activity logging, health evaluation, streak tracking, reporting, and data persistence.

## Overview

Personal Fitness & Health Tracker enables users to record and manage key health activities, analyze fitness trends, maintain healthy streaks, and export activity history for reporting and analysis.

The application supports tracking:

- Steps Walked
- Water Intake
- Sleep
- BMI
- Resting Heart Rate

All activity records are persisted locally and remain available across application restarts.

---

## Features

### Activity Management

Users can:

- Add new activity records
- View current streak on individual activity
- Edit existing activity records
- Delete activity records
- View complete activity history
- View filtered history by activity or date
- Search activity logs using partial or exact matches

Supported activity types:

| Activity Type | Unit |
|--------------|------|
| Steps Walked | Steps |
| Water Intake | Liters |
| Sleep | Hours |
| BMI | BMI Value |
| Resting Heart Rate | BPM |

---

### Activity Validation

The application performs validation before saving activity data.

#### Date Validation

- Future dates are not allowed.
- Invalid date entries are rejected.

#### Activity-Specific Validation

| Activity Type | Validation Rule |
|--------------|----------------|
| Steps Walked | Value must be greater than 0 |
| Sleep | Value must be between 0 and 24 hours |
| Water Intake | Value must be between 0 and 10 liters |
| BMI | Value must be within the configured BMI range |
| Resting Heart Rate | Value must be between 20 and 150 BPM |

Invalid records are prevented from being persisted.

---

### Health Evaluation

Daily health status is calculated using activity specific evaluation rules.

#### Evaluation Logic

| Activity Type | Evaluation Method |
|--------------|------------------|
| Steps Walked | Daily total steps |
| Water Intake | Daily total water intake |
| Sleep | Daily total sleep hours |
| BMI | Latest BMI recorded |
| Resting Heart Rate | Daily average resting heart rate |


---

### Today's Summary

Provides a real-time snapshot of today's activity metrics.

#### Summary Includes

- Total activity value
- Average activity value
- Latest activity value

Updates automatically when:

- Activities are added
- Activities are edited
- Activities are deleted

---

### Search & Filtering

Users can quickly locate activity records using advanced search and filtering options.

#### Search By

- Activity Name
- Date
- Activity Type
- Activity Value

#### Filter By

- Activity Type
- Date

Additional capabilities:

- View complete activity history
- Support for partial matches
- Support for exact matches

---

### CSV Export

Export activity history for analysis and reporting.

#### Export Features

- CSV format export
- Timestamp based file names

Example:

```text
ActivityHistory_20250615_103015.csv
```

---

### Data Persistence

Activity records are stored locally to ensure data is retained between application sessions.

#### Persistence Features

- Automatic file creation
- Local storage management
- Activity history retained after restart
- No manual setup required

---

### File Storage Enhancement

Application-generated files are stored in:

```text
Documents\FitnessTracker
```

The application automatically:

- Creates the FitnessTracker folder if it does not exist
- Stores activity data files
- Stores exported CSV reports

#### Stored Files

```text
Documents
└── FitnessTracker
    ├── activities.json
    └── ActivityHistory_YYYYMMDD_HHMMSS.csv
```

---

## Conclusion

Personal Fitness & Health Tracker provides a complete solution for daily health monitoring by combining activity tracking, health evaluation, streak management, reporting, CSV exports, local persistence into a single user-friendly application.