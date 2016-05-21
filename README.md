# Hello！
欢迎来到Smart On Line 项目，这里是一个电子商城B2C项目的源代码页面。<br>
该项目使用了ASP.NET技术和MSSQL数据库<br> 
Welcome to my ASP.NET project, here is an electronic mall B2C project source code page.

### 代码说明
Smart.sql.......数据库导入文件,2016/5/21号有修改，请重新导入数据库<br>
SqlHelper.cs....数据库助手类<br>
CheckCode.cs....验证码生成类<br>
WareListView....商品展示页面<br>
ShoppingCart2...购物车页<br>
Order...........订单页面<br>
ProDataList.....带输入参数和输出参数实例<br>
DataListPaging..分页实例<br> 
### ________________________________________________________
ter分支上。<br>
$ git push -u origin master<br>
但依然没能解决问题<br>
+++++++++++++++++++++++++++++++++++++++++++<br>
出现错误的主要原因是github中的README.md文件不在本地代码目录中<br>

可以通过如下命令进行代码合并【注：pull=fetch+merge]<br>
git pull --rebase origin master<br>
执行上面代码后可以看到本地代码库中多了README.md文件<br>

此时再执行语句 git push -u origin master即可完成代码上传到github<br>
