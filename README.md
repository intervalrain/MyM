# Introduction
- **具備收入、支出、轉帳的記帳功能**
- **具備計算機作為輸入介面**
- **具備 summary page 顯示本年、本月、本週、本日收入支出**
- **具備不同類型帳戶(Cash, Bank) 顯示總額**
- **每個帳戶可以顯示細項**
- ... (持續增加中)

# My Practice Stack
## Methodology
- **Clean Architecture**
- **CQRS**
- **Event-Driven-Model**
- **Example Mapping**
- **OOAD (Object-Oriented Analysis and Design)**
- **ATDD (Acceptance Test-Driven Development)**
- **DDD (Domain-Driven Design)**

## Tech Stack
- **C#/.Net** (後端) 
- **PostgreSQL** (資料庫)
- **.Net Core Web API (Interface Adapter) 
- **Swift UI** (前端)

# Clean Architecture
Clean Architecture 是一種軟體設計架構模式，旨在保持系統的高內聚、低耦合和可測試性。它的核心概念是將系統劃分為多個層次，每個層次都負責特定的功能和關注點，並且層次之間的依賴方向是內向的，即內層不依賴外層。

## 層次劃分
1. **Domain Layer/Entity Layer（實體層）**:
    - 定義業務邏輯和規則的核心對象。這些實體是應用程序中最重要的部分，不應該受到其他層的影響。
  
2. **Application Layer/Usecase Layer（用例層）**:
    - 定義應用程序的具體業務邏輯。用例層使用實體來執行應用程序的具體操作，並且不依賴於框架、UI 或外部系統。
    - 使用 CQRS
  
3. **Presentation/Interface Adapters（介面適配層）**:
    - 將外部系統、UI、數據庫等轉換為用例和實體層可以使用的格式。這一層通常包括控制器、視圖模型和數據傳輸對象（DTO）。
    - 使用 RESTful Web API
  
4. **Infrastructure/Franework and drivers（框架和驅動層）**:
    - 包含框架和工具，例如 UI 框架、資料庫驅動等。這一層的變化不應影響其他層次。

## Test
1. **IntegrationTests**
2. **Application.UnitTests**
3. **Domain.UnitTests**
4. **SubcutaneousTests**

## 優點
- **可測試性**: 各層之間的依賴關係是單向的，這使得單元測試和集成測試變得更加容易。
- **靈活性**: 可以輕鬆替換技術實現，例如更換數據庫或 UI 框架，而不會影響業務邏輯。
- **維護性**: 清晰的層次劃分使得代碼結構更加清晰，易於維護和擴展。

# Example Mapping

Example Mapping 是一種協作技術，旨在幫助團隊快速構建具有清晰需求的用例。這個過程通常包括以下步驟：
1. **用例描述**: 團隊成員描述用例的主要功能和目標。
2. **示例**: 團隊成員提供具體的示例來說明用例的行為。
3. **規則**: 團隊成員定義應用用例的業務規則。
4. **問題**: 團隊成員提出並解決可能出現的問題和挑戰。

# OOAD (Object-Oriented Analysis and Design)

面向對象分析與設計是一種軟件設計方法，強調使用對象和類來構建軟件系統。這種方法通過定義對象的屬性、行為和關係，來實現軟件的模塊化和可重用性。

# ATDD (Acceptance Test-Driven Development)

驗收測試驅動開發是一種開發方法，強調在編寫代碼之前先編寫驗收測試。這些測試通常由業務分析師、測試人員和開發人員共同編寫，以確保系統的功能滿足業務需求。

# DDD (Domain-Driven Design)

領域驅動設計是一種軟件設計方法，強調通過關注領域專家和開發團隊之間的合作來構建軟件系統。DDD 強調使用統一語言來描述系統的業務邏輯，並通過分離領域邏輯和技術實現來保持系統的靈活性和可維護性。
