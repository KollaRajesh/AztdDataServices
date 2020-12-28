echo "Enter the Resource Group name:" &&
read resourceGroupName &&
echo "Enter the location (i.e. centralus):" &&
read location &&
az deployment group create --resource-group $resourceGroupName --template-uri "./template/az-cosmos-template.json"  --parameters "./template/az-cosmos-parameters.json"
