const fs = require('fs');
const https = require('https');

function downloadHtml(fileUrl, outputPath) {

  https.get(fileUrl, (res) => {
    if (res.statusCode !== 200) {
      console.error(`Failed to get '${fileUrl}' (${res.statusCode})`);
      res.resume();
      return;
    }
    const fileStream = fs.createWriteStream(outputPath);
    res.pipe(fileStream);
    fileStream.on('finish', () => {
      fileStream.close();
      console.log(`Saved to ${outputPath}`);
    });
  }).on('error', (err) => {
    console.error(`Error: ${err.message}`);
  });
}

for(var i = 0; i<87; i++) {
	const htmlUrl = 'https://www.toratemetfreeware.com/online/f_01355_part_'+ (i + 1) +'.html';
	const savePath = 'book/'+ i +'.html';

	downloadHtml(htmlUrl, savePath);
}