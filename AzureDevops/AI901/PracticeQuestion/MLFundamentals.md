# AI-901 Notes - Module 2: Machine Learning Fundamentals

---

# What is Machine Learning?

Machine Learning (ML) is a branch of Artificial Intelligence (AI) where computers learn patterns from data instead of being explicitly programmed.

## Traditional Programming

```
Input + Rules → Output
```

Example:

```
Marks = 95

if Marks >= 35
    Result = Pass
else
    Result = Fail
```

The developer writes all the rules.

---

## Machine Learning

```
Historical Data
        ↓
Machine Learning Algorithm
        ↓
Model
        ↓
Prediction
```

Instead of writing rules, we provide data and let the computer learn the patterns.

---

# Real-World Examples

| Example | Machine Learning Task |
|----------|-----------------------|
| Predict house price | Regression |
| Predict loan approval | Classification |
| Detect spam email | Classification |
| Recommend movies | Recommendation |
| Predict stock demand | Regression |
| Fraud detection | Classification |

---

# Types of Machine Learning

Machine Learning is mainly divided into three types:

```
Machine Learning

├── Supervised Learning

├── Unsupervised Learning

└── Reinforcement Learning
```

---

# 1. Supervised Learning

## Definition

The model learns using **labeled data**.

Labeled means the correct answer is already known.

### Example

| Hours Studied | Passed? |
|--------------|----------|
| 2 | No |
| 3 | No |
| 5 | Yes |
| 8 | Yes |

Since we already know the answers, the model learns the relationship.

---

## Real-Life Examples

- Email Spam Detection
- Predict Loan Approval
- Disease Prediction
- House Price Prediction
- Student Pass/Fail Prediction

---

## Supervised Learning Types

```
Supervised Learning

├── Classification

└── Regression
```

---

# Classification

Classification predicts categories or labels.

### Examples

- Spam or Not Spam
- Approved or Rejected
- Fraud or Genuine
- Cat or Dog
- Positive or Negative Review

### Output

```
Yes / No

True / False

Cat / Dog

Approved / Rejected
```

Classification always predicts a category.

---

# Regression

Regression predicts a numeric value.

### Example

- House Price
- Temperature
- Salary
- Sales
- Rainfall

### Output

```
₹5,00,000

30°C

$4500

1000 Units
```

Regression predicts numbers.

---

# Classification vs Regression

| Classification | Regression |
|---------------|------------|
| Predicts Category | Predicts Number |
| Yes/No | ₹500000 |
| Spam/Not Spam | Temperature |
| Fraud/Not Fraud | House Price |
| Positive/Negative | Sales Forecast |

---

# 2. Unsupervised Learning

## Definition

The model learns from **unlabeled data**.

No correct answers are given.

The model discovers hidden patterns.

---

## Example

Suppose a shopping website has customer data.

```
Customer 1

Customer 2

Customer 3

Customer 4

Customer 5
```

Nobody tells the computer who belongs together.

The ML model groups similar customers automatically.

---

# Clustering

Grouping similar items together.

Example:

```
Customers

↓

Cluster 1
Young Customers

Cluster 2
Business Customers

Cluster 3
Premium Customers
```

---

## Real-Life Examples

- Customer Segmentation
- Product Grouping
- Market Analysis
- Group Similar Documents
- News Categorization

---

# 3. Reinforcement Learning

## Definition

The model learns by interacting with an environment.

It receives:

- Reward ✅
- Penalty ❌

The goal is to maximize rewards.

---

## Example

Robot Learning

```
Move Right

Reward +10

↓

Move Left

Penalty -5
```

The robot gradually learns the best path.

---

## Real-Life Examples

- Self-driving Cars
- Robotics
- Chess AI
- Game Playing
- Drone Navigation

---

# Azure Machine Learning

Azure Machine Learning is Microsoft's cloud service for building, training, deploying, and managing machine learning models.

### Features

- Train models
- Test models
- Deploy models
- Monitor models
- Manage datasets
- Automate ML workflows

---

# Practice Questions

## Question 1

A bank wants to predict whether a customer will repay a loan.

Which type of Machine Learning should be used?

A. Reinforcement Learning

B. Classification

C. Clustering

D. OCR

### Answer

✅ **B. Classification**

**Explanation:** The output is either "Repay" or "Not Repay", which is a category.

---

## Question 2

A company wants to predict the selling price of a house.

A company wants to predict the selling price of a house.

Which type of Machine Learning should be used?

A. Classification

B. Regression

C. Clustering

D. NLP

### Answer

✅ **B. Regression**

**Explanation:** The output is a numeric value (price).

---

## Question 3

A retail company wants to divide customers into different groups based on purchasing habits.

Which Machine Learning technique should they use?

A. Regression

B. Classification

C. Clustering

D. OCR

### Answer

✅ **C. Clustering**

**Explanation:** Clustering groups similar data without predefined labels.

---

## Question 4

A robot learns to walk by receiving rewards for correct movements and penalties for incorrect ones.

Which type of Machine Learning is this?

A. Supervised Learning

B. Unsupervised Learning

C. Reinforcement Learning

D. Regression

### Answer

✅ **C. Reinforcement Learning**

**Explanation:** The robot improves its behavior using rewards and penalties.

---

## Question 5

A company has historical email data labeled as "Spam" and "Not Spam". It wants to train a model to classify future emails.

Which type of learning is being used?

A. Supervised Learning

B. Unsupervised Learning

C. Reinforcement Learning

D. Clustering

### Answer

✅ **A. Supervised Learning**

**Explanation:** The training data is labeled with the correct answers.

---

# Exam Tips

## Remember This Table

| Scenario | Answer |
|----------|--------|
| Predict Yes/No | Classification |
| Predict a Number | Regression |
| Group Similar Items | Clustering |
| Learn from Rewards | Reinforcement Learning |
| Labeled Data | Supervised Learning |
| Unlabeled Data | Unsupervised Learning |

---

# AI-901 Exam Cheat Sheet

```
Machine Learning

│

├── Supervised Learning
│     │
│     ├── Classification → Categories
│     └── Regression → Numeric Values
│
├── Unsupervised Learning
│     │
│     └── Clustering → Groups Similar Data
│
└── Reinforcement Learning
      │
      └── Rewards & Penalties
```

---

# Quick Revision

- **Machine Learning** enables computers to learn from data.
- **Supervised Learning** uses labeled data.
- **Classification** predicts categories.
- **Regression** predicts numeric values.
- **Unsupervised Learning** uses unlabeled data.
- **Clustering** groups similar items.
- **Reinforcement Learning** learns through rewards and penalties.
- **Azure Machine Learning** is used to build, train, deploy, and manage ML models.