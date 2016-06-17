using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsPartsReconstruccion.Models;
using System.Configuration;

namespace CarsPartsReconstruccion.Controllers
{
    public class SupplierPieceController : Controller
    {
        int CarPartReconstructionId = Convert.ToInt32(ConfigurationManager.AppSettings["CarPartReconstructionId"]);
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /SupplierPiece/

        public ActionResult Index(int supplierId)
        {
            var supplierpieces = db.SupplierPieces
                                    .Where(sp => sp.supplierId == supplierId)
                                    .Include(s => s.Piece).Include(s => s.Supplier).ToList();

            var supplierpiecesAver = supplierpieces.Select(spiece =>
            {
                spiece.AverageSuppliersPrice = db.SupplierPieces.Where(sp => sp.pieceId == spiece.pieceId && sp.supplierId != CarPartReconstructionId).Average(spa => (decimal?)spa.price); return spiece;
            });

            var supplier = db.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                ViewBag.SupplierName = supplier.supplierName;
                ViewBag.SupplierId = supplier.supplierId;
            }
            return View(supplierpiecesAver);
        }

        public ActionResult IndexPiece(int pieceId)
        {
            var supplierpieces = db.SupplierPieces
                                    .Where(sp => sp.pieceId == pieceId)
                                    .Include(s => s.Piece).Include(s => s.Supplier).ToList()
                                    .OrderBy(sp => sp.price).OrderByDescending(sp => (sp.existence > 0 ? 1 : 0));

            var piece = db.Pieces.Find(pieceId);
            if (piece != null)
            {
                ViewBag.PieceName = piece.pieceName;
                ViewBag.PieceId = piece.pieceId;
                ViewBag.PiecePrice = piece.piecePrice;
            }
            return View(supplierpieces);
        }

        //
        // GET: /SupplierPiece/Details/5

        public ActionResult Details(int supplierId, int pieceId)
        {
            SupplierPiece supplierpiece = db.SupplierPieces.Where(sp => sp.pieceId == pieceId && sp.supplierId == supplierId).FirstOrDefault();
            if (supplierpiece == null)
            {
                return HttpNotFound();
            }
            return View(supplierpiece);
        }

        //
        // GET: /SupplierPiece/Create

        public ActionResult Create(int supplierId)
        {
            var model = new SupplierPiece() { supplierId = supplierId };

            ViewBag.pieceId = new SelectList(
                db.Pieces.Where(pi => !db.SupplierPieces.Any(sp => sp.supplierId == supplierId && sp.pieceId == pi.pieceId))
                , "pieceId", "pieceName");

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierId),
                "supplierId", "supplierName", supplierId);

            return View(model);
        }

        //
        // POST: /SupplierPiece/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierPiece supplierpiece)
        {
            if (ModelState.IsValid)
            {
                db.SupplierPieces.Add(supplierpiece);
                db.SaveChanges();

                //Find the Piece
                var piece = db.Pieces.Find(supplierpiece.pieceId);
                //Obtain the average price
                var average = (decimal?)db.SupplierPieces.Where(sp => sp.pieceId == supplierpiece.pieceId && sp.supplierId != CarPartReconstructionId).Average(sp => (decimal?)sp.price);
                if (average != null)
                {
                    //Verify if the difference between reference price and the average of the suppliers prices exceed the five percent
                    var twoPercent = (decimal)(piece.piecePrice * 0.02m);
                    if (Math.Abs((decimal)(piece.piecePrice - average)) > twoPercent)
                    {
                        var referencePrice = piece.piecePrice;
                        piece.piecePrice = Math.Round((decimal)average, 2);

                        return RedirectToAction("UpdatePiecePrice",
                            new { pieceId = piece.pieceId, supplierId = supplierpiece.supplierId, averagePrice = average });
                    }
                }

                return RedirectToAction("Index", new { supplierId = supplierpiece.supplierId });
            }

            ViewBag.pieceId = new SelectList(
                db.Pieces.Where(pa => !db.SupplierPieces.Any(sp => sp.supplierId == supplierpiece.supplierId && sp.pieceId == pa.pieceId))
                , "pieceId", "pieceName", supplierpiece.pieceId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpiece.supplierId), "supplierId", "supplierName", supplierpiece.supplierId);

            return View(supplierpiece);
        }


        public ActionResult UpdatePiecePrice(int pieceId, int supplierId, decimal averagePrice)
        {
            var model = db.Pieces.Find(pieceId);
            ViewBag.PiecePrice = model.piecePrice;
            ViewBag.supplierId = supplierId;
            model.piecePrice = averagePrice;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePiecePrice(Piece piece, int supplierId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piece).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { supplierId = supplierId });
            }
            return View(piece);
        }


        //
        // GET: /SupplierPiece/Edit/5

        public ActionResult Edit(int supplierId, int pieceId)
        {
            SupplierPiece supplierpiece = db.SupplierPieces.Where(sp => sp.pieceId == pieceId && sp.supplierId == supplierId).FirstOrDefault();
            if (supplierpiece == null)
            {
                return HttpNotFound();
            }

            ViewBag.pieceId = new SelectList(
                db.Pieces.Where(pa => pa.pieceId == pieceId || !db.SupplierPieces.Any(sp => sp.supplierId == supplierpiece.supplierId && sp.pieceId == pa.pieceId))
                , "pieceId", "pieceName", supplierpiece.pieceId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpiece.supplierId), "supplierId", "supplierName", supplierpiece.supplierId);

            return View(supplierpiece);
        }

        //
        // POST: /SupplierPiece/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierPiece supplierpiece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierpiece).State = EntityState.Modified;
                db.SaveChanges();

                //Find the Piece
                var piece = db.Pieces.Find(supplierpiece.pieceId);
                //Obtain the average price
                var average = (decimal?)db.SupplierPieces.Where(sp => sp.pieceId == supplierpiece.pieceId && sp.supplierId != CarPartReconstructionId).Average(sp => (decimal?)sp.price);
                if (average != null)
                {
                    //Verify if the difference between reference price and the average of the suppliers prices exceed the five percent
                    var twoPercent = (decimal)(piece.piecePrice * 0.02m);
                    if (Math.Abs((decimal)(piece.piecePrice - average)) > twoPercent)
                    {
                        var referencePrice = piece.piecePrice;
                        piece.piecePrice = Math.Round((decimal)average, 2);

                        return RedirectToAction("UpdatePiecePrice",
                            new { pieceId = piece.pieceId, supplierId = supplierpiece.supplierId, averagePrice = average });
                    }
                }

                return RedirectToAction("Index", new { SupplierId = supplierpiece.supplierId });
            }

            ViewBag.pieceId = new SelectList(
                db.Pieces.Where(pa => pa.pieceId == supplierpiece.pieceId || !db.SupplierPieces.Any(sp => sp.supplierId == supplierpiece.supplierId && sp.pieceId == pa.pieceId))
                , "pieceId", "pieceName", supplierpiece.pieceId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpiece.supplierId), "supplierId", "supplierName", supplierpiece.supplierId);

            return View(supplierpiece);
        }

        //
        // GET: /SupplierPiece/Delete/5

        public ActionResult Delete(int supplierId, int pieceId)
        {
            SupplierPiece supplierpiece = db.SupplierPieces.Where(sp => sp.pieceId == pieceId && sp.supplierId == supplierId).FirstOrDefault();
            if (supplierpiece == null)
            {
                return HttpNotFound();
            }
            return View(supplierpiece);
        }

        //
        // POST: /SupplierPiece/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int supplierId, int pieceId)
        {
            SupplierPiece supplierpiece = db.SupplierPieces.Where(sp => sp.pieceId == pieceId && sp.supplierId == supplierId).FirstOrDefault();
            db.SupplierPieces.Remove(supplierpiece);
            db.SaveChanges();

            //Find the piece
            var piece = db.Pieces.Find(supplierpiece.pieceId);
            //Obtain the average price
            var average = (decimal?)db.SupplierPieces.Where(sp => sp.pieceId == supplierpiece.pieceId && sp.supplierId != CarPartReconstructionId).Average(sp => (decimal?)sp.price);
            if (average != null)
            {
                //Verify if the difference between reference price and the average of the suppliers prices exceed the five percent
                var twoPercent = (decimal)(piece.piecePrice * 0.02m);
                if (Math.Abs((decimal)(piece.piecePrice - average)) > twoPercent)
                {
                    var referencePrice = piece.piecePrice;
                    piece.piecePrice = Math.Round((decimal)average, 2);

                    return RedirectToAction("UpdatePiecePrice",
                        new { pieceId = piece.pieceId, supplierId = supplierpiece.supplierId, averagePrice = average });
                }
            }

            return RedirectToAction("Index", new { SupplierId = supplierpiece.supplierId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}