const fs = require('fs');
const cheerio = require('cheerio');
const iconv = require('iconv-lite');

let bookTitles = [];

for (var i = 0; i < 87; i++) {
  const chapter = 'book/' + i + '.html';

  const buffer = fs.readFileSync(chapter);
  // Decode using windows-1255
  const html = iconv.decode(buffer, 'windows-1255');
  const $ = cheerio.load(html);

  // Find all <span> elements with the exact style "font-size:19px"
  const spans31 = $('span[style]').filter(function () {
    // Normalize the style attribute for reliable matching
    const style = $(this).attr('style').replace(/\s+/g, '').toLowerCase();
    return style.includes('font-size:31px');
  });

  // Find all <span> elements with the exact style "font-size:19px"
  const spans19 = $('span[style]').filter(function () {
    // Normalize the style attribute for reliable matching
    const style = $(this).attr('style').replace(/\s+/g, '').toLowerCase();
    return style.includes('font-size:19px');
  });

  let bookSubTitles = [];
  spans19.each((j, el) => {
    bookSubTitles.push({
      id: j,
      title: $(el).text().trim()
    });
  });


  bookTitles.push({
    id: i ,
    title: spans31.text().trim(),
    bookSubTitles
  });

}

// console.log(JSON.stringify(bookTitles));
 fs.writeFileSync('bookStructure.json', JSON.stringify(bookTitles));



