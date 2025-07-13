# 🎓 AWS CDK 學習專案 - YARP 反向代理

## 專案目標
通過建立 YARP 反向代理專案，循序漸進地學習 AWS CDK 和核心 AWS 服務

## 📋 學習規則
- ✅ 一次只學習一個概念
- ✅ 每個步驟都有明確的學習目標
- ✅ 每個步驟完成後進行驗證
- ✅ 不跳過任何重要概念

## 🚀 學習計劃 (預計 10-12 週)

### 階段一：CDK 基礎 (1-2 週)
**學習目標**：掌握 CDK 基本概念和操作

#### 步驟 1: 驗證 CDK 環境
- [x] 確認 CDK 版本 (2.1020.2)
- [x] 確認 AWS CLI 配置
- [x] 設定預設 region (ap-northeast-2)
- [x] 更新學習計劃到 README.md
- [x] Git commit 學習進度 ✅ (commit: 9f0da17)
- [ ] 理解 CDK 專案結構
- [ ] 學習 CDK 基本指令

#### 步驟 2: 建立第一個簡單的 VPC
- [x] 學習 VPC 的基本概念
- [x] 用 CDK 建立最簡單的 VPC
- [x] 階段 2A: 理解專案結構 ✅
- [x] 階段 2B-1: 編譯專案成功 ✅
- [x] 階段 2B-2: 生成 CloudFormation 模板 (cdk synth) ✅
- [x] 階段 2B-3: 查看變更 (cdk diff) ✅
- [x] 階段 2B-3.1: 成本優化修改 (移除 NAT Gateway) ✅
- [x] 階段 2B-4a: CDK Bootstrap (環境初始化) ✅
- [x] 階段 2B-4b: 部署到 AWS (cdk deploy) ✅
- [x] 階段 2C: 驗證部署結果 ✅
- [x] 理解 CloudFormation 輸出 ✅

#### 🎉 步驟 2 完全完成！

#### 步驟 3: 理解 CDK 的 Infrastructure as Code
- [x] 學習 CDK 與 CloudFormation 的關係 ✅
- [x] 理解 Stack 和 Construct 概念 ✅
- [x] 練習修改和重新部署 ✅
- [x] 體驗 Infrastructure as Code 的威力 ✅
- [x] 理解成本影響分析 ✅

**🎉 步驟 3 完全完成！** 

**重要學習成果**：
- ✅ 一行程式碼修改觸發複雜基礎設施變化
- ✅ CDK 智慧推論：PRIVATE_WITH_EGRESS → NAT Gateway + EIP
- ✅ 成本意識：NAT Gateway 月費 ~$75，選擇成本優化
- ✅ 最佳實務：自動跨 AZ 部署確保高可用性
- ✅ 學習策略：在學習階段控制成本，保持 PRIVATE_ISOLATED

### 階段二：網路基礎 (2-3 週)
**學習目標**：掌握 AWS 網路服務

#### 步驟 4: 建立 Subnet 和 Route Table
- [x] 學習 Public/Private Subnet 概念 ✅
- [x] 實作三層網路架構 ✅
- [x] 理解路由表的配置 ✅
- [ ] 測試子網路連通性 ← 下一步

**🎉 步驟 4A 完成！**

**重要理解成果**：
- ✅ 路由表數量：7個 (6個CDK + 1個Default)
- ✅ Public vs Private 差異：0.0.0.0/0 路由的存在
- ✅ 流量路徑：Public (local→IGW→外部) vs Private (timeout)
- ✅ 網路隔離實現：透過路由表控制而非物理隔離

**🎉 步驟 4B: 架構設計思維 完成！**

**進階架構理解**：
- ✅ 三層架構設計：ALB(Public) → ECS(App) → RDS(Database)
- ✅ 高可用性：跨 AZ 部署避免單點故障
- ✅ 安全性分層：透過 Subnet 隔離 + Security Groups 控制
- ✅ 成本最佳化：VPC Endpoints vs NAT Gateway 權衡分析
- ✅ 合規性考量：資料庫完全隔離，便於審計和監控

#### 步驟 5: 網路閘道優化
- [x] 學習 Internet Gateway 概念 ✅
- [x] 理解 NAT Gateway 作用 ✅
- [x] 學習 VPC Endpoints 概念 ✅
- [x] 實作 VPC Endpoints 進行成本優化 ✅

**🎉 步驟 5C: 實作 S3 Gateway Endpoint 完成！**

**完成成果**：
- ✅ 在 CDK 中新增 S3 Gateway Endpoint 程式碼
- ✅ 執行 `cdk diff` 觀察基礎設施變化
- ✅ 分析路由表的自動更新機制
- ✅ 理解免費 Gateway Endpoint 的價值
- ✅ 掌握 CDK 子網路選擇邏輯 (`SubnetGroupName`)
- ✅ 驗證 App 和 Database 子網路都正確關聯

**重要學習成果**：
- CDK 子網路選擇：`SubnetType` vs `SubnetGroupName`
- AWS 服務代管：自動路由表更新機制
- Infrastructure as Code：精確控制 vs 智慧推論
- 網路架構實務：私有子網路的 S3 存取優化

**🎉 步驟 5A: VPC Endpoints 理論完成！**

**重要概念掌握**：
- ✅ Gateway Endpoints：免費，支援 S3/DynamoDB，保持私有 + 節省成本
- ✅ Interface Endpoints：付費($0.01/小時)，支援大多數 AWS 服務
- ✅ 成本比較：Interface Endpoints ($14.4/月) vs NAT Gateway ($34/月)
- ✅ 架構影響：直接私有連接 vs 透過 Internet 路由

**🎉 步驟 5B: 成本效益分析完成！**

**深度理解**：
- ✅ 適用場景：2-5個 AWS 服務時 Interface Endpoints 更划算
- ✅ 必須使用 NAT Gateway：第三方服務、軟體包下載、固定IP需求
- ✅ 混合策略：開發階段彈性，生產階段成本優化
- ✅ YARP 專案建議：Gateway(S3) + Interface(ECR+CloudWatch) ≈ $21.6/月

#### 步驟 6: 實作 Security Groups
- [x] 學習防火牆規則 ✅
- [x] 設定安全群組 ✅
- [x] 理解最小權限原則 ✅
- [x] 測試安全群組規則 ✅

**🎉 步驟 6 完全完成！**

**實作成果**：
- ✅ 建立 ALB Security Group (HTTP/HTTPS from Internet)
- ✅ 建立 ECS Security Group (TCP/8080 from ALB only)
- ✅ 實現 Security Group 引用機制
- ✅ 應用最小權限原則
- ✅ 成功部署並驗證設定

**重要學習成果**：
- 🛡️ **防火牆規則設計**：Ingress/Egress 的精確控制
- 🔗 **Security Group 引用**：動態安全控制的威力
- 🎯 **最小權限原則**：只開放必要的存取
- 🚀 **Infrastructure as Code**：程式化的安全管理

### 階段三：運算服務 (2-3 週)
**學習目標**：掌握容器化服務

#### 步驟 7: 建立 ECS Cluster
- [x] 學習容器編排概念 ✅
- [x] 建立 ECS Cluster (Fargate 模式) ✅
- [x] 理解 Fargate vs EC2 模式 ✅
- [x] 驗證 Cluster 部署成功 ✅

**🎉 步驟 7 完全完成！**

**重要學習成果**：
- ✅ ECS Cluster 是**邏輯容器**，本身免費
- ✅ CDK 命名規則：自動加入 Stack 名稱和唯一 ID
- ✅ Fargate 模式：serverless 容器運行環境
- ✅ Infrastructure as Code：漸進式建構複雜架構

#### 步驟 8: 建立第一個 ECS Service
- [x] 學習 Task Definition 概念 ✅
- [x] 部署簡單的 Hello World 容器 ✅
- [x] 驗證容器運行狀態 ✅
- [x] 理解 ECS Service 概念 ✅
- [x] 解決 Fargate Port 配置問題 ✅
- [x] 修正網路架構（PRIVATE_WITH_EGRESS）✅
- [x] 處理映像拉取網路依賴 ✅

**🎉 步驟 8 完全完成！**

**重要學習成果**：
- ✅ **Task Definition vs Service**：藍圖 vs 生命週期管理
- ✅ **Fargate 網路限制**：awsvpc 模式的 Port 配置
- ✅ **映像拉取機制**：網路架構對容器的實際影響
- ✅ **CloudFormation 依賴**：資源建立順序和重試機制
- ✅ **成本優化思維**：MaxAzs=1 節省 50% NAT Gateway 費用
- ✅ **錯誤診斷技能**：從 Task 失敗到根本原因分析

#### 步驟 9: IAM 權限設定
- [x] 建立 ECS Task Role ✅
- [x] 建立 ECS Execution Role ✅
- [x] 理解最小權限原則 ✅
- [x] 測試權限設定 ✅
- [x] 學習 ServicePrincipal 概念 ✅
- [x] 掌握 CDK 依賴管理 ✅
- [x] 處理容器健康檢查配置 ✅

**🎉 步驟 9 完全完成！**

**重要學習成果**：
- ✅ **IAM 雙重角色架構**：Task Role（應用程式權限）vs Execution Role（ECS 服務權限）
- ✅ **ServicePrincipal 信任關係**：`ecs-tasks.amazonaws.com` 的作用和來源
- ✅ **最小權限原則實踐**：精確的 S3 存取控制 (`arn:aws:s3:::bucket/*`)
- ✅ **CDK 依賴管理最佳實務**：避免過度的顯式依賴
- ✅ **容器映像工具依賴**：nginx 映像的極簡特性
- ✅ **錯誤處理和快速修復**：CloudFormation 回滾機制

### 階段四：負載平衡器 (1-2 週)
**學習目標**：掌握流量分配

#### 步驟 10: 建立 Application Load Balancer
- [x] 學習負載平衡器概念 ✅
- [x] 配置 ALB 和 Target Group ✅
- [x] 設定健康檢查 ✅
- [x] 測試負載平衡功能 ✅

**🎉 步驟 10 完全完成！**

**本次學習重點與驗證成果**：
- ✅ ALB 必須跨兩個 AZ 部署，否則 CloudFormation 會報錯（"At least two subnets in two different Availability Zones must be specified"）
- ✅ 成功調整 VPC 設定，讓 Public Subnet 跨兩個 AZ
- ✅ ALB 狀態 active，Target Group 健康檢查為 healthy
- ✅ ALB DNS 可正常顯示 nginx 歡迎頁面，流量順利導向 ECS Task
- ✅ 熟悉 ALB、Target Group、Listener、Security Group 的正確設計與關聯

#### 步驟 11: 網路整合測試
- [x] 測試 ALB → ECS 連通性 ✅
- [x] 驗證安全群組規則 ✅
- [ ] 測試故障轉移
- [ ] 理解網路流量路徑

**🎉 步驟 11A: 網路整合測試 進行中！**

**實作成果**：
- ✅ ALB → ECS 連通性測試完成，流量正常導向 nginx 容器
- ✅ Security Groups 規則驗證通過：ALB SG 允許 Internet 存取，ECS SG 僅允許來自 ALB 的流量
- ✅ 建立 YarpProxy 專案：包含 Header Transform 和 Domain Header Service 功能
- ✅ 建立 YarpTarget 專案：WeatherForecast API 作為後端服務
- ✅ 配置 YARP 反向代理：YarpProxy → YarpTarget (localhost:5128) 路由設定完成
- ✅ 本機測試完成：YarpTarget 服務正常運行於 localhost:5128
- ✅ 反向代理功能驗證：YarpProxy 成功轉發請求到後端服務
- ✅ Header Transform 功能確認：X-Source → Source header 對應正常運作
- ✅ API 端點測試：/source 端點成功回傳自定義 header 資訊

### 階段五：YARP 應用 (2-3 週)
**學習目標**：實作具體應用

#### 步驟 12: 建立 YARP 專案
- [x] 學習 YARP 基本概念 ✅
- [x] 建立簡單的反向代理 ✅
- [x] 建立後端測試服務 ✅
- [x] 本機測試 YARP 反向代理功能 ✅
- [x] 修改 YARP 配置適應容器環境 ✅
- [x] 學習 Docker 容器化 ✅
- [x] 建立 YarpProxy Dockerfile ✅
- [x] 建立 YarpTarget Dockerfile ✅
- [x] 修正 Port 配置一致性 ✅
- [x] 本機測試 Docker 容器 ✅
  - [x] YarpTarget 容器建構和測試成功 ✅
  - [x] YarpProxy 容器建構和測試 ✅
  - [x] 容器間通訊測試成功 ✅
  - [x] Docker Compose 整合測試成功 ✅
- [x] 推送映像到 ECR ✅
  - [x] 建立 yarp-proxy ECR 儲存庫 ✅
  - [x] 建立 yarp-target ECR 儲存庫 ✅
  - [x] ECR 登入和身份驗證 ✅
  - [x] 映像重新標記 (Tag) ✅
  - [x] 推送映像到 ECR ✅
  - [x] 驗證映像推送成功 ✅

**🎉 步驟 12 完全完成！**

**重要學習成果**：
- ✅ **Docker 容器化**：掌握多階段構建和映像優化
- ✅ **Docker Compose**：成功模擬 ECS Task Definition 環境
- ✅ **容器間通訊**：解決網路命名空間和服務發現問題
- ✅ **ECR 整合**：完成映像推送和雲端儲存庫管理
- ✅ **多平台映像**：理解 Image Index 和平台架構概念
- ✅ **成本控制**：在免費額度內完成雲端部署準備

**技術架構里程碑**：
- 🐳 本機容器化開發環境完成
- ☁️ 雲端映像儲存庫部署完成
- 🔄 反向代理應用程式容器化完成
- 🎯 準備進入 ECS 部署階段

#### 步驟 12A: ECS 部署更新（優先處理）
- [x] 更新 CDK Task Definition 使用 ECR 映像 ✅
- [x] 修改容器定義支援 YARP 雙容器架構 ✅
- [x] 配置容器間通訊 (localhost) ✅
- [x] 更新 Port 和健康檢查設定 ✅
- [x] 配置 CDK Context 映像 URI ✅
- [ ] 部署新的 Task Definition 到 ECS ← **當前任務**
- [ ] 驗證 ALB → ECS → YARP 完整鏈路
- [ ] 測試反向代理功能

#### 步驟 12B: CloudWatch Logs配置 🎯
**學習目標**: 配置容器日誌收集，學習ECS logging機制

### 🎯 學習重點  
**為什麼需要Logs？**
- 調試容器啟動問題的關鍵工具
- 生產環境監控必備
- 了解應用程式運行狀況

**ECS Logging架構：**
```
Container → CloudWatch Logs → Log Groups → Log Streams
```

### 📚 技術知識
**CloudWatch Logs基本概念：**
1. **Log Group**: 日誌的邏輯容器 (例：/ecs/yarp-task)
2. **Log Stream**: 同一來源的日誌序列 (例：yarp-proxy/task-id)  
3. **Log Driver**: 負責收集和傳送日誌 (awslogs)

**CDK配置要素：**
1. 導入CloudWatch Logs namespace
2. 建立Log Groups  
3. 在容器定義中配置Logging
4. 確保ExecutionRole有CloudWatch權限

### 🎯 本步驟任務
**階段一：了解現狀與規劃**

請先回答以下思考題：
1. 目前我們的容器為什麼看不到任何logs？
2. 在ECS中，哪個角色負責將logs寫入CloudWatch？
3. 我們需要為幾個容器配置logging？

**完成思考後，我們將進行：**
1. 檢查必要的CDK imports
2. 建立CloudWatch Log Groups  
3. 修改容器定義添加logging配置
4. 驗證ExecutionRole權限

### ⚠️ 學習注意事項
- CloudWatch Logs會產生少量費用
- Log retention期間可以配置
- 過多的logs會影響成本

**準備好開始了嗎？請先思考上述問題，然後告訴我你的理解！** 🤔

### ✅ 思考題回答檢討
1. **預設不log** ✓ - ECS需要明確配置LogDriver
2. **ExecutionRole** ✓ - 負責容器啟動階段的CloudWatch寫入
3. **2個容器** ✓ - yarp-proxy和yarp-target都需要logging配置

### 🛠️ 階段二：CDK實作配置

**任務1: 檢查並添加必要的imports**
- 檢查是否已導入 `Amazon.CDK.AWS.Logs`
- 了解 LogGroup 和 LogDriver 的概念

**任務2: 建立CloudWatch Log Groups**
- 為 YARP 服務建立專用的 Log Group
- 配置適當的 retention period

**任務3: 修改容器定義**
- 在 `AddYarpProxy` 方法中添加 Logging 配置
- 在 `AddYarpTarget` 方法中添加 Logging 配置
- 使用 `awslogs` driver

**任務4: 驗證ExecutionRole權限**
- 確認 CloudWatch Logs 寫入權限

### ⚠️ 決定延後 CloudWatch Logs 配置
**用戶決定**: 由於成本考量，暫時跳過 logging 配置，優先解決部署問題
**未來計畫**: 實際需要時再添加 logging 機制

---

## Step 12A-2: 修正Task Definition資源配置 🎯
**學習目標**: 解決記憶體不足問題，成功部署雙容器架構

### 🔍 問題診斷
- **失敗症狀**: Exit Code 139 (SIGSEGV/Segmentation Fault)
- **根本原因**: 256 CPU + 512MB 記憶體不足以支撐雙.NET應用程式
- **解決方案**: 增加資源配置

### 📚 Fargate資源配置知識
**CPU 和記憶體組合限制:**
- 256 CPU 單位 → 512MB - 2GB 記憶體  
- 512 CPU 單位 → 1GB - 4GB 記憶體
- 1024 CPU 單位 → 2GB - 8GB 記憶體

**我們的選擇:** 512 CPU + 1024MB (1GB)
- 符合 .NET 應用程式需求
- 平衡效能與成本
- 為雙容器預留充足資源

### 🎯 任務：修改Task Definition
請找到 `CreateTaskDef()` 方法並修改資源配置：

**目前配置:**
```csharp
Cpu = 256,
MemoryLimitMiB = 512,
```

**修改為:**
```csharp
Cpu = 512,
MemoryLimitMiB = 1024,
```

完成後執行 `cdk deploy` 驗證部署是否成功！

### ❌ 部署結果：還是記憶體不足！
- **配置**: 512 CPU + 1024MB
- **結果**: yarp-proxy Exit Code 139 (相同錯誤)
- **診斷**: 雙.NET應用程式需要更多記憶體

### 🎯 Step 12A-3: 第二次資源調整
**現實**: 1024MB 對雙.NET容器還是不夠！

**解決方案選項:**

**選項A: 提高到推薦配置**
```csharp
Cpu = 1024,              // 1 vCPU
MemoryLimitMiB = 2048,   // 2GB
```
- 每個容器約1GB記憶體
- 符合.NET生產環境最佳實踐
- 成本: 約每小時多$0.02

**選項B: 中等配置**
```csharp
Cpu = 512,               // 0.5 vCPU  
MemoryLimitMiB = 1536,   // 1.5GB
```
- 每個容器約750MB記憶體
- 平衡成本與穩定性
- 可能還是有風險

### 💡 技術顧問建議
選擇**選項A (1024 CPU + 2048MB)**：
- 🎯 一次到位，避免重複調整
- 📈 符合.NET應用程式標準配置  
- 💰 成本增加有限，但穩定性大幅提升

**你想選擇哪個選項？**

### ❌ 第三次失敗：1024 CPU + 2048MB 還是 Exit Code 139！

**重新診斷：可能不是記憶體問題！**

Exit Code 139 (SIGSEGV) 的其他可能原因：

### 🔍 可能的根本問題

**1. 容器映像問題**
- Debug模式建置 (應該用Release)
- 缺少必要的.NET Runtime
- Windows/Linux平台不匹配
- 映像損壞

**2. 程式碼問題**  
- 應用程式啟動就crash
- 不正確的依賴注入配置
- 空指標引用 (Null Reference)
- 執行緒問題

**3. 環境配置問題**
- 環境變數設置錯誤
- Port綁定問題 (應用程式無法綁定到port 80)
- 網路配置衝突

**4. Fargate特定問題**
- Platform架構不匹配 (x86 vs ARM)
- 容器運行時版本問題
- 權限不足

### 🎯 下一步診斷計畫

**階段1: 本地容器測試** 
驗證映像本身是否正常：
```bash
docker run -p 80:80 yarp-proxy:latest
```

**階段2: 檢查程式碼**
確認應用程式可以正常啟動和綁定port 80

**階段3: CloudWatch Logs**  
是時候啟用logging了，這是唯一能看到真正錯誤的方法

**階段4: 簡化測試**
先用單一nginx容器測試ECS部署

### 🤔 建議立即行動
1. **本地測試容器** (排除映像問題)
2. **啟用CloudWatch Logs** (看到真實錯誤訊息)
3. **簡化為單容器** (隔離問題)

**你想從哪個診斷步驟開始？**

### ✅ 架構問題確認！找到根本原因！

**診斷結果:**
- **映像架構**: `amd64` (x86_64) 
- **Task Definition**: 沒有指定CPU架構
- **Fargate預設**: 可能使用ARM架構
- **結果**: 架構不匹配導致Exit Code 139

### 🔧 解決方案：指定Runtime Platform

在 `CreateTaskDef()` 方法中添加 `RuntimePlatform` 配置：

**修改前:**
```csharp
var taskDefinition = new TaskDefinition(this, "MyTaskDefinition", new TaskDefinitionProps
{
    Cpu = "1024",
    MemoryMiB = "2048",
    Compatibility = Compatibility.FARGATE,
    TaskRole = CreateTaskRole(),
    ExecutionRole = CreateExecutionRole()
});
```

**修改後:**
```csharp  
var taskDefinition = new TaskDefinition(this, "MyTaskDefinition", new TaskDefinitionProps
{
    Cpu = "1024",
    MemoryMiB = "2048",
    Compatibility = Compatibility.FARGATE,
    RuntimePlatform = new RuntimePlatform  // ✅ 正確的架構配置方法
    {
        CpuArchitecture = CpuArchitecture.X86_64,
        OperatingSystemFamily = OperatingSystemFamily.LINUX
    },
    TaskRole = CreateTaskRole(),
    ExecutionRole = CreateExecutionRole()
});
```

### 🎯 下一步
修改CDK代碼後執行 `cdk deploy` 驗證修正結果！

---

## Step 12A-3: 架構拆分計畫 💡
**學習目標**: 將基礎設施與運算資源分離，實現成本控制與開發靈活性

### 🏗️ 拆分架構設計 (已修正)
**智慧決策**: 分離無成本基礎資源與有成本運算資源

⚠️ **重要修正**: ALB確實有固定成本(約$16/月)，應重新分類

### 選項1: 最大成本控制 (推薦)

**InfrastructureStack (純基礎設施)**:
- VPC + Subnets
- Security Groups
- **特點**: 真正的零成本基礎設施

**ApplicationStack (所有運算資源)**:
- Application Load Balancer + Target Groups
- ECS Cluster + Service + Task Definition  
- **特點**: 包含所有成本，可完全刪除

### 選項2: 平衡穩定性與成本

**NetworkStack (網路層)**:
- VPC + Subnets + Security Groups
- Application Load Balancer + Target Groups
- **特點**: 網路入口點穩定，DNS不變

**ApplicationStack (運算層)**:
- ECS Cluster + Service + Task Definition
- **特點**: 主要運算成本，可獨立部署

### ✅ 已選定：選項1 - 最大成本控制

**最終架構設計:**

**InfrastructureStack (純基礎設施)**:
- VPC + Subnets
- Security Groups
- **成本**: $0 (真正零成本)
- **用途**: 長期保留的網路基礎

**ApplicationStack (完整應用)**:
- Application Load Balancer + Target Groups
- ECS Cluster + Service + Task Definition
- **成本**: 所有費用來源
- **用途**: 實驗時可完全刪除，節省100%成本

### 💡 選擇優勢
- 🎯 **零成本保留**: InfrastructureStack真正免費
- 💰 **完全控制**: 一鍵刪除所有費用
- 🔄 **重建簡單**: DNS變更可接受，成本節省更重要

### 💰 成本控制優勢
- 🎯 **精準控制**: 只需刪除ApplicationStack即可停止主要費用
- 🔄 **開發靈活**: 保留基礎設施，隨時可重新部署應用
- 📈 **擴展性**: 多個應用可共享同一基礎設施
- 🛡️ **風險隔離**: 實驗不會影響基礎網路架構

### 🎯 實作計畫
**執行時機**: 當前部署成功並驗證功能正常後

1. **建立 InfrastructureStack.cs**
2. **建立 ApplicationStack.cs**  
3. **配置 Cross-Stack References**
4. **更新 Program.cs 部署邏輯**
5. **獨立部署命令測試**

**部署命令將變為:**
```bash
cdk deploy InfrastructureStack    # 部署基礎設施
cdk deploy ApplicationStack       # 部署應用程式
```

### ⏳ 下一步
等待當前部署完成 → 驗證YARP功能 → 執行架構拆分

---

#### 步驟 13: S3 整合
- [ ] 學習 S3 基本操作
- [ ] 建立 S3 Bucket
- [ ] 實作 domain mapping 功能
- [ ] 測試動態配置更新

#### 步驟 14: ServiceB 後端服務
- [ ] 建立簡單的後端服務
- [ ] 配置 YARP 路由規則
- [ ] 測試端到端流量
- [ ] 驗證 header 注入功能

### 階段六：監控和維護 (1-2 週)
**學習目標**：掌握運維技能

#### 步驟 15: 設定 CloudWatch
- [ ] 學習監控概念
- [ ] 配置日誌群組
- [ ] 設定自定義指標
- [ ] 建立 CloudWatch Dashboard

#### 步驟 16: 成本最佳化
- [ ] 設定成本警示
- [ ] 分析成本分布
- [ ] 實施自動擴展
- [ ] 驗證成本節省效果

## 🛠️ 常用 CDK 指令

```bash
# 編譯專案
dotnet build src

# 查看變更
cdk diff

# 部署到 AWS
cdk deploy

# 生成 CloudFormation 模板
cdk synth

# 清理資源
cdk destroy
```

## 📊 成本預估

```
目標架構成本 (月費用)：
- ECS Fargate Spot: $15-25
- Application Load Balancer: $16
- S3 (domain mapping): $0-1
- CloudWatch Logs: $0-2
- 總計: $31-44/月
```

## 🎯 當前進度

**階段一：CDK 基礎** ✅ **完成**
- [x] 步驟 1: 環境驗證 ✅
- [x] 步驟 2: 第一個 VPC ✅  
- [x] 步驟 3: Infrastructure as Code 概念 ✅

**階段二：網路基礎** ✅ **完成**
- [x] 步驟 4: 建立 Subnet 和 Route Table ✅
- [x] 步驟 5: 網路閘道優化 ✅
- [x] 步驟 6: 實作 Security Groups ✅

**階段三：運算服務** ✅ **完成**
- [x] 步驟 7: 建立 ECS Cluster ✅
- [x] 步驟 8: 建立第一個 ECS Service ✅
- [x] 步驟 9: IAM 權限設定 ✅

**階段四：負載平衡器** 🔄 **進行中**
- [x] 步驟 10: 建立 Application Load Balancer ✅
- [x] 步驟 11: 網路整合測試 (進行中) ✅

**階段五：YARP 應用** 🔄 **開始**
- [x] 步驟 12: 建立 YARP 專案 (進行中) ✅

**目前狀態**: 
- ✅ ALB → ECS 架構完成並通過連通性和安全測試
- ✅ 建立 YarpProxy 和 YarpTarget 專案，配置完成
- 🔄 下一步：本機測試 YARP 反向代理功能

**🎉 重要里程碑達成**: 階段二：網路基礎 完全完成！成功建立完整的 AWS 網路架構，準備進入運算服務學習！

## 🎉 恭喜！步驟 5：網路閘道優化 完全完成！

您已經成功：
- ✅ 完全理解 VPC Endpoints 的價值和原理
- ✅ 深入分析成本效益權衡
- ✅ 實作 S3 Gateway Endpoint
- ✅ 掌握 CDK 子網路選擇邏輯
- ✅ 驗證 AWS 服務的自動路由表更新
- ✅ 體驗 Infrastructure as Code 的精確控制

**重要架構理解**：
- 🔄 **Gateway vs Interface Endpoints**：成本 vs 功能權衡
- 🧠 **CDK 智慧推論**：`SubnetType` vs `SubnetGroupName`
- 🔧 **AWS 服務代管**：自動路由表更新機制
- 💰 **成本優化策略**：免費 Gateway 優於付費 Interface

**準備進入步驟 6：Security Groups**！您即將學習 AWS 網路安全的核心組件！

## 🎉 恭喜！階段二：網路基礎 完全完成！

您已經成功建立了完整的 AWS 網路架構：
- ✅ **VPC 架構**：三層網路設計 (Public/App/Database)
- ✅ **路由控制**：深入理解路由表運作機制
- ✅ **成本優化**：S3 Gateway Endpoint 免費私有存取
- ✅ **安全防護**：Security Groups 最小權限設計
- ✅ **智慧整合**：Security Group 引用動態控制

**重要架構技能**：
- 🏗️ **網路分層**：Public/Private 子網路隔離
- 🔒 **安全設計**：多層次防護機制
- 💰 **成本意識**：Gateway vs Interface Endpoints 權衡
- 🎯 **最佳實務**：AWS Well-Architected 網路原則

**CDK 進階技能**：
- 🧠 **Infrastructure as Code**：程式化的基礎設施管理
- 🔄 **智慧推論**：CDK 自動決策與手動控制的平衡
- 🎯 **精確控制**：SubnetGroupName vs SubnetType 的選擇
- 🚀 **版本控制**：基礎設施變更的追蹤和管理

**準備進入階段三：運算服務**！您即將學習 ECS 容器編排的核心技能！

## 🎉 恭喜！階段一：CDK 基礎 完全完成！

您已經成功：
- ✅ 設定開發環境
- ✅ 配置 AWS CLI  
- ✅ 建立學習計劃
- ✅ 學會基本的 Git 操作
- ✅ 理解 CDK 專案結構
- ✅ 掌握 CDK 基本指令
- ✅ 完成 CDK Bootstrap
- ✅ 部署第一個 VPC
- ✅ 應用成本優化思維
- ✅ 驗證 AWS 資源
- ✅ 深度理解 Infrastructure as Code 概念
- ✅ 體驗 CDK 的智慧決策引擎

**準備進入階段二：網路基礎**: 現在可以深入學習路由表的配置和運作原理！

## 📍 Region 重要說明

**ap-northeast-2 (首爾) 的優勢**:
- 低延遲 (適合亞洲地區)
- 完整的 AWS 服務支援
- 相對較低的成本

**成本預估調整 (ap-northeast-2)**:
```
目標架構成本 (月費用，首爾地區)：
- ECS Fargate Spot: $16-28
- Application Load Balancer: $18
- S3 (domain mapping): $0-1
- CloudWatch Logs: $0-2
- 總計: $34-49/月
```

## 🚀 下一步操作指南 - 步驟 2: VPC 部署

**💡 環境變數疑問解答**

**Q: 沒有設定 CDK_DEFAULT_ACCOUNT 和 CDK_DEFAULT_REGION 會有問題嗎？**
**A: 不會！** 對於學習階段完全沒問題：

✅ **目前階段（學習用）**：
- `cdk synth` 會正常工作
- `cdk deploy` 會使用您的 AWS CLI 預設設定
- 模板會是 environment-agnostic（通用型）

🔧 **將來可能需要設定的情況**：
- 當您需要查詢特定 region 的 AZ 數量
- 當您需要多環境部署（dev/staging/prod）
- 當您的程式碼需要 region/account 特定的功能

**階段 2A: 理解 CDK 專案結構**

首先，讓我們了解您的專案結構：
```
AwsCdkStack/
├── src/AwsCdkStack/
│   ├── Program.cs           ← CDK 應用程式進入點
│   ├── AwsCdkStackStack.cs  ← 您的 VPC 程式碼在這裡
│   └── AwsCdkStack.csproj   ← 專案相依性
├── cdk.json                 ← CDK 配置檔
└── README.md               ← 學習進度追蹤
```

**階段 2B: CDK 基本指令實作**

1. **編譯專案** (確保程式碼正確):
   ```bash
   cd src
   dotnet build
   ```
   📝 **學習重點**: C# 編譯過程，檢查語法錯誤

2. **🔄 生成 CloudFormation 模板** (理解 Infrastructure as Code) - **下一步**:

   **⚠️ 重要概念：環境變數和 CDK 模式**
   
   您的 Program.cs 使用了環境變數：
   ```csharp
   Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
   Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
   ```
   
   **如果沒有設定環境變數會怎樣？**
   - ✅ `cdk synth` **仍然會工作**
   - ✅ CDK 會使用 **environment-agnostic** 模式
   - ✅ 生成的模板可以部署到任何 region/account
   - ⚠️ 但某些功能會受限（如 AZ 查詢、AMI 查詢等）
   
   **cdk synth 的作用**:
   ```bash
   cdk synth
   ```
   📝 **這個指令做什麼**:
   - 🔄 將您的 C# CDK 程式碼轉換成 CloudFormation JSON
   - 🔍 驗證程式碼語法和邏輯
   - 📄 生成 AWS 實際執行的基礎設施定義
   - 💾 輸出到 `cdk.out/` 資料夾
   
   📝 **學習重點**: 理解 Infrastructure as Code 的轉換過程
   💡 **觀察要點**: 
   - 輸出的 JSON 結構
   - VPC 相關的資源定義
   - CDK 自動生成的資源名稱
   - 注意任何警告訊息

3. **查看將要建立的資源** (部署前檢查):
   ```bash
   cdk diff
   ```
   📝 **學習重點**: 基礎設施變更追蹤和版本控制

4. **部署到 AWS** (實際建立資源):
   ```bash
   cdk deploy
   ```
   📝 **學習重點**: CloudFormation Stack 建立過程

**階段 2C: 驗證和學習**

5. **AWS Console 驗證**:
   - 🌐 登入 [AWS Console](https://ap-northeast-2.console.aws.amazon.com/vpc/)
   - 📍 確認在 ap-northeast-2 region
   - 🔍 查看 VPC 服務 → 檢查您的 VPC
   - 📊 查看 CloudFormation → 檢查 Stack 狀態

6. **記錄學習成果**:
   - VPC ID 和 CIDR Block
   - 建立了哪些子網路
   - Route Table 配置
   - 任何觀察到的有趣現象

**⚠️ 重要學習提醒**:
- 📚 每個指令執行後，觀察輸出並提問
- 💰 注意成本：VPC 本身免費，但要留意 NAT Gateway
- 🔒 確保有適當的 IAM 權限
- ⏱️ 首次部署可能需要 5-10 分鐘
- 📝 隨時在 README.md 中記錄問題和發現

---

## 📚 學習資源

- [AWS CDK Developer Guide](https://docs.aws.amazon.com/cdk/v2/guide/)
- [AWS Well-Architected Framework](https://aws.amazon.com/architecture/well-architected/)
- [YARP Documentation](https://microsoft.github.io/reverse-proxy/)