//path to SAM/CloudFormation template
const templatefilePath = 'Api/serverless.template';

// transform template
var fs = require('fs');
var template = fs.readFileSync(templatefilePath, 'utf8');
// setting the CodeUri path to the publish folder for lambda function to correctly find the entry point
template = template.replace(/"CodeUri": "",/g, '"CodeUri": "bin/Release/netcoreapp2.1/publish/",');
template = template.replace(/"@@DATE@@"/g, Date.now());
fs.writeFileSync(templatefilePath, template);
console.info("template updated");

// reading environment name from build machine and setting it in template-params.json to be able to pass the environment name to cloudformation
var envName = process.env.EnvironmentName;
var paramFile = fs.readFileSync("template-params.json");
var params = JSON.parse(paramFile);
params.Parameters.EnvironmentName = envName;
fs.writeFileSync("template-params.json", JSON.stringify(params));