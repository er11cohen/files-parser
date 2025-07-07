const fs = require('fs');
const iconv = require('iconv-lite');



for (var i = 0; i < 87; i++) {
  const buffer = fs.readFileSync('sourceBook/' + i + '.html');

  // Decode using windows-1255
  const html = iconv.decode(buffer, 'windows-1255');
   const modified = html.replace(/font-size/g, 'fz')
                    .replace(/&nbsp;/g, '')
                    .replace(/&nbsp/g, '')
                    .replace(/src/g, 's')
                    .replace(/href/g, 'hf');
                    
   const encoded = iconv.encode(modified, 'windows-1255');

   fs.writeFileSync('resultBook/' + i + '.html', encoded);
}